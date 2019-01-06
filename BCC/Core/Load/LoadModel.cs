using BCC.Interface_View.StandardInterface.Tension;
using BCC.Miscs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BCC.Core.Load
{
    enum Dummy { DUMMY }

    public abstract class PredefinedGroupContainer
    {
        private List<Enum> parameters;
        private List<Tuple<Func<string>, Dictionary<Enum, double>>> predefines;
        private readonly bool isCustomable;

        public PredefinedGroupContainer(List<Enum> parameters, bool isCustomable)
        {
            this.parameters = new List<Enum>(parameters);
            this.predefines = new List<Tuple<Func<string>, Dictionary<Enum, double>>>();
            this.isCustomable = isCustomable;
        }

        protected void PushPredefine(Func<string> nameCall, Dictionary<Enum, double> values)
        {
            foreach(var param in parameters)
            {
                if (!values.Keys.Contains(param)) throw new Exception("Not enough parameters");
            }
            foreach(var param in values.Keys)
            {
                if (!parameters.Contains(param)) throw new Exception("Unknonw parameter");
            }
            predefines.Add(new Tuple<Func<string>, Dictionary<Enum, double>>(nameCall, values));
        }

        public List<Tuple<Func<string>, Dictionary<Enum, double>>> Predefines => 
            new List<Tuple<Func<string>, Dictionary<Enum, double>>>(predefines);
        public List<Enum> Parameters => new List<Enum>(parameters);
        public bool IsCustomable => isCustomable;
    }

    abstract class LoadModel : Model
    {
        // The view and the control for geometry computation
        protected LoadMenu dimensioningPart = null;
        
        public static class StaticFields
        {
            public static readonly int PARAM_BOX_WIDTH = 175;
            public static readonly int PARAM_BOX_HEIGHT = 50;
            public static readonly int MARGIN = 3;
            public static readonly int VALUE_PRECISION = 3;
            public static readonly int TOLERANCE_PRECISION = 5;
            public static readonly double TRUE = 1.0, FALSE = 2.0;
            public static readonly double NULL = double.NegativeInfinity;
        }

        //protected abstract List<LoadParams>

        public Dictionary<UserControl, Func<string>> GetMenus()
        {
            if(dimensioningPart is null)
            {
                var parameterControls = new List<Control>();
                var getterCalls = new Dictionary<Enum, Func<int, double>>();
                var setterCalls = new Dictionary<Enum, Action<int, double>>();
                var paramBoxWidth = StaticFields.PARAM_BOX_WIDTH;
                var paramBoxHeight = StaticFields.PARAM_BOX_HEIGHT;
                var margin = StaticFields.MARGIN;
                var toolTip = new ToolTip();
                var nameCallGenerators = NameCallGenerators();

                new Action(() =>
                {
                    var tabIndex = 0;

                    // MaterialGroupBoxes
                    new Action(() =>
                    {
                        foreach (var pair in PredefinedGroups())
                        {
                            var GroupBox = new GroupBox
                            {
                                Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                                Location = new Point(3, 3),
                                Size = new Size(paramBoxWidth * 2 + 20, paramBoxHeight * 2 + 20),
                                TabIndex = tabIndex++,
                                TabStop = false
                            };
                            Vocabulary.AddNameCall(() =>
                            {
                                GroupBox.Text = nameCallGenerators[pair.Key]();
                            });
                            var FlowPanel = new FlowLayoutPanel()
                            {
                                Location = new Point(margin, paramBoxHeight + margin),
                                Size = new Size(paramBoxWidth * 2 + 20 - 2 * margin, 58),
                            };

                            var predefines = pair.Value.Predefines;
                            var ComboBox = new ComboBox()
                            {
                                FormattingEnabled = true,
                                Location = new Point(7, 22),
                                Size = new Size(121, 24),
                                TabIndex = 0
                            };
                            Vocabulary.AddNameCall(() =>
                            {
                                var index = ComboBox.SelectedIndex;
                                ComboBox.Items.Clear();
                                if(pair.Value.IsCustomable) ComboBox.Items.Add(Vocabulary.ParameterLabels.Dimensioning.Custom());
                                foreach (var param in predefines)
                                {
                                    ComboBox.Items.Add(param.Item1());
                                }
                                ComboBox.SelectedIndex = index >= 0 ? index : 0;
                            });
                            GroupBox.Controls.Add(ComboBox);
                            GroupBox.Controls.Add(FlowPanel);

                            var subInputs = new Dictionary<Enum, NumericUpDown>();
                            foreach (var param in pair.Value.Parameters)
                            {
                                var j = 0;
                                var SubGroupBox = new GroupBox()
                                {
                                    Location = new Point(margin + (margin + paramBoxWidth) * (j++), margin),
                                    Size = new Size(paramBoxWidth, paramBoxHeight),
                                    TabIndex = j,
                                    TabStop = false
                                };
                                Vocabulary.AddNameCall(() =>
                                {
                                    SubGroupBox.Text = nameCallGenerators[param]();
                                });
                                var UpDown = new NumericUpDown()
                                {
                                    DecimalPlaces = StaticFields.VALUE_PRECISION,
                                    Increment = (decimal)Math.Pow(10, 1 - StaticFields.VALUE_PRECISION),
                                    Location = new Point(margin, 21),
                                    Maximum = 50000,
                                    Minimum = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                                    Size = new Size(120, 22),
                                    TabIndex = 0,
                                    Value = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                                };
                                SubGroupBox.Controls.Add(UpDown);
                                FlowPanel.Controls.Add(SubGroupBox);
                                subInputs.Add(param, UpDown);
                            }
                            
                            parameterControls.Add(GroupBox);

                            ComboBox.SelectedIndexChanged += (sender, e) =>
                            {
                                var index = ComboBox.SelectedIndex;
                               
                                if(index == 0)
                                {
                                    foreach(var subInput in subInputs)
                                    {
                                        subInput.Value.Enabled = true;
                                    }
                                }
                                else
                                {
                                    var values = predefines[index - 1].Item2;
                                    foreach (var subInput in subInputs)
                                    {
                                        subInput.Value.Value = (decimal)values[subInput.Key];
                                        subInput.Value.Enabled = false;
                                    }
                                }
                            };
                            getterCalls.Add(pair.Key, i =>
                            {
                                if(subInputs.Count == 0)
                                {
                                    if (i == 0) return ComboBox.SelectedIndex;
                                    else return Model.NULL;
                                }
                                var called = subInputs.ToList().Find(p => Convert.ToInt32(p.Key) == i);
                                if (called.Equals(default(KeyValuePair<Enum, NumericUpDown>))) return Model.NULL;
                                else return (double)called.Value.Value;
                            });
                            setterCalls.Add(pair.Key, (i, value) =>
                            {
                                if (subInputs.Count == 0) return;
                                var called = subInputs.ToList().Find(p => Convert.ToInt32(p.Key) == i);
                                if (!called.Equals(default(KeyValuePair<Enum, NumericUpDown>)))
                                {
                                    if(called.Value.Enabled)
                                    {
                                        called.Value.Value = (decimal)value;
                                    }
                                }
                            });
                        }
                    })();

                    // GearParametersGroupBoxes
                    new Action(() =>
                    {
                        int subTabIndex = 0;
                        // 
                        // GearParametersGroupBox
                        // 
                        var GearParametersGroupBox = new GroupBox
                        {
                            Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(3, 3),
                            Size = new Size(370, 134),
                            TabIndex = tabIndex++,
                            TabStop = false
                        };
                        var GearParametersFlowPanel = new FlowLayoutPanel
                        {
                            Location = new Point(3, 18),
                            Size = new Size(364, 113),
                            Dock = DockStyle.Fill,
                            TabIndex = 0
                        };
                        GearParametersGroupBox.Controls.Add(GearParametersFlowPanel);
                        // Obligatory integer parameters
                        foreach(var param in ObligatoryIntParams())
                        {
                            var GroupBox = new GroupBox()
                            {
                                Location = new Point(3, 3),
                                Size = new Size(175, 49),
                                TabIndex = subTabIndex++,
                                TabStop = false
                            };
                            Vocabulary.AddNameCall(() => GroupBox.Text = nameCallGenerators[param]());
                            var UpDown = new NumericUpDown()
                            {
                                DecimalPlaces = 0,
                                Increment = 1,
                                Location = new Point(7, 22),
                                Maximum = 100,
                                Minimum = 1,
                                Size = new Size(120, 22),
                                TabIndex = 0,
                                Value = 1
                            };
                            GroupBox.Controls.Add(UpDown);
                            GearParametersFlowPanel.Controls.Add(GroupBox);
                            getterCalls.Add(param, i =>
                            {
                                if (i == 0) return (double)UpDown.Value;
                                else return Model.NULL;
                            });
                            setterCalls.Add(param, (i, value) =>
                            {
                                if (i == 0) UpDown.Value = (decimal)value;
                            });
                        }
                        // Obligatory floating-point parameters
                        foreach(var param in ObligatoryFloatParams())
                        {
                            var GroupBox = new GroupBox()
                            {
                                Location = new Point(3, 3),
                                Size = new Size(175, 49),
                                TabIndex = subTabIndex++,
                                TabStop = false
                            };
                            Vocabulary.AddNameCall(() => GroupBox.Text = nameCallGenerators[param]());
                            var UpDown = new NumericUpDown()
                            {
                                DecimalPlaces = StaticFields.VALUE_PRECISION,
                                Increment = (decimal)Math.Pow(10, 1 - StaticFields.VALUE_PRECISION),
                                Location = new Point(7, 22),
                                Maximum = 1000,
                                Minimum = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                                Size = new Size(120, 22),
                                TabIndex = 0,
                                Value = (decimal)Math.Pow(10, -StaticFields.VALUE_PRECISION),
                            };
                            GroupBox.Controls.Add(UpDown);
                            GearParametersFlowPanel.Controls.Add(GroupBox);
                            getterCalls.Add(param, i =>
                            {
                                if (i == 0) return (double)UpDown.Value;
                                else return Model.NULL;
                            });
                            setterCalls.Add(param, (i, value) =>
                            {
                                if (i == 0) UpDown.Value = (decimal)value;
                            });
                        }
                        parameterControls.Add(GearParametersGroupBox);
                    })();

                    // TolerancesGroupBox
                    new Action(() =>
                    {
                        
                        Enum enumSelected = Dummy.DUMMY;
                        Mutex mu = new Mutex();
                        Enum EnumSelected()
                        {
                            mu.WaitOne();
                            var ret = enumSelected;
                            mu.ReleaseMutex();
                            return ret;
                        }
                        void SelectEnum(Enum e)
                        {
                            mu.WaitOne();
                            enumSelected = e;
                            mu.ReleaseMutex();
                        }


                        // Memory set for fit fields
                        var polyFits = new Dictionary<Enum, Tuple<double, double>[]>();
                        var singleFits = new Dictionary<Enum, Tuple<double, double>>();

                        // List for int->enum casting
                        var enumsIndexers = new List<Enum>();

                        // GroupBox for whole tolerances micrmenu
                        var GroupBox = new GroupBox()
                        {
                            Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(3, 3),
                            Size = new Size(367, 213),
                            TabIndex = tabIndex++,
                            TabStop = false,
                        };
                        Vocabulary.AddNameCall(() =>
                        {
                            GroupBox.Text = Vocabulary.ParameterLabels.Dimensioning.EngineeringTolerances();
                        });
                        
                        // ListBox for fit type choosing
                        var ListBox = new ListBox
                        {
                            FormattingEnabled = true,
                            ItemHeight = 16,
                            Location = new Point(6, 21),
                            Size = new Size(303, 116),
                            TabIndex = 8
                        };
                        Vocabulary.AddNameCall(() =>
                        {
                            var index = ListBox.SelectedIndex;
                            ListBox.Items.Clear();
                            foreach (var param in PluralFitParams())
                            {
                                ListBox.Items.Add(nameCallGenerators[param]());
                            }
                            foreach(var param in SingularFitParams())
                            {
                                ListBox.Items.Add(nameCallGenerators[param]());
                            }
                            ListBox.SelectedIndex = index < 0 ? 0 : index;
                        });
                        
                        // NumericUpDown for changing the index of the tolerated element
                        var IndexUpDown = new NumericUpDown
                        {
                            Location = new Point(255, 143),
                            Maximum = 0,
                            Minimum = 0,
                            RightToLeft = RightToLeft.Yes,
                            Size = new Size(54, 22),
                            TabIndex = 10,
                            UpDownAlign = LeftRightAlignment.Left,
                            Value = 0
                        };
                        // NumericUpDowns for tolerating choosen element
                        var LowerUpDown = new NumericUpDown
                        {
                            DecimalPlaces = StaticFields.TOLERANCE_PRECISION,
                            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(41, 177),
                            Size = new Size(120, 26),
                            TabIndex = 9,
                            Minimum = -10,
                            Maximum = 10,
                            Increment = (decimal)Math.Pow(10, 1 - StaticFields.TOLERANCE_PRECISION),
                        };
                        var UpperUpDown = new NumericUpDown
                        {
                            DecimalPlaces = StaticFields.TOLERANCE_PRECISION,
                            Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(41, 145),
                            Size = new Size(120, 26),
                            TabIndex = 8,
                            Minimum = -10,
                            Maximum = 10,
                            Increment = (decimal)Math.Pow(10, 1 - StaticFields.TOLERANCE_PRECISION),
                        };
                        // Variables preventing redundant usages of ValueChangedHandlers
                        bool skipup = false, skipdown = false;

                        // EventHandlers
                        IndexUpDown.ValueChanged += (sender, e) =>
                        {
                            var param = EnumSelected();
                            skipup = skipdown = true;
                            if (polyFits.ContainsKey(param))
                            {
                                UpperUpDown.Value = (decimal)polyFits[param][(int)IndexUpDown.Value].Item1;
                                LowerUpDown.Value = (decimal)polyFits[param][(int)IndexUpDown.Value].Item2;
                            }
                            else
                            {
                                UpperUpDown.Value = (decimal)singleFits[param].Item1;
                                LowerUpDown.Value = (decimal)singleFits[param].Item2;
                            }
                        };
                        var UDValueChanged = new EventHandler((sender, e) =>
                        {
                            var param = EnumSelected();
                            if (polyFits.ContainsKey(param))
                            {
                                polyFits[param][(int)IndexUpDown.Value] =
                                new Tuple<double, double>((double)UpperUpDown.Value,(double)LowerUpDown.Value);
                            }
                            else
                            {
                                singleFits[param] = new Tuple<double, double>((double)UpperUpDown.Value, (double)LowerUpDown.Value);
                            }
                        });
                        UpperUpDown.ValueChanged += (sender, e) =>
                        {
                            if (skipup) skipup = false;
                            else UDValueChanged(sender, e);
                        };
                        UpperUpDown.GotFocus += (sender, e) => skipup = false;
                        LowerUpDown.ValueChanged += (sender, e) =>
                        {
                            if (skipdown) skipdown = false;
                            else UDValueChanged(sender, e);
                        };
                        LowerUpDown.GotFocus += (sender, e) => skipdown = false;
                        ListBox.SelectedIndexChanged += (sender, e) =>
                        {
                            var param = enumsIndexers[ListBox.SelectedIndex];
                            SelectEnum(param);
                            skipup = skipdown = true;
                            if (polyFits.ContainsKey(param))
                            {
                                UpperUpDown.Value = (decimal)polyFits[param][(int)IndexUpDown.Value].Item1;
                                LowerUpDown.Value = (decimal)polyFits[param][(int)IndexUpDown.Value].Item2;
                            }
                            else
                            {
                                UpperUpDown.Value = (decimal)singleFits[param].Item1;
                                LowerUpDown.Value = (decimal)singleFits[param].Item2;
                            }
                        };

                        var UpperDeviationLabel = new Button
                        {
                            Enabled = false,
                            Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(6, 140),
                            Margin = new Padding(0),
                            Size = new Size(32, 32),
                            TabIndex = 11,
                            Text = "+",
                            TextAlign = ContentAlignment.BottomCenter,
                            UseVisualStyleBackColor = true
                        };
                        var LowerDeviationLabel = new Button
                        {
                            Enabled = false,
                            Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 238),
                            Location = new Point(6, 172),
                            Margin = new Padding(0),
                            Size = new Size(32, 32),
                            TabIndex = 12,
                            Text = "-",
                            TextAlign = ContentAlignment.BottomCenter,
                            UseVisualStyleBackColor = true
                        };

                        void SetIndexUpDownMaximum(object value)
                        {
                            if(dimensioningPart.InvokeRequired)
                            {
                                SetValueCallBack d = new SetValueCallBack(SetIndexUpDownMaximum);
                                dimensioningPart.Invoke(d, new object[] { value });
                            }
                            else
                            {
                                int maximum = (int)value;
                                if (IndexUpDown.Value > maximum) IndexUpDown.Value = maximum;
                                IndexUpDown.Maximum = maximum;
                            }
                        }

                        var indexUpdater = new Thread(() =>
                        {
                            int n = 0;
                            Enum param = Dummy.DUMMY;
                            while (ListBox.Items.Count == 0) Thread.Sleep(100);
                            while(true)
                            {
                                var tempParam = EnumSelected();
                                var temp = ToleratedElementsQuantity();
                                if(temp != n || tempParam != param)
                                {
                                    if (polyFits.ContainsKey(tempParam))
                                    {
                                        SetIndexUpDownMaximum(temp);
                                    }
                                    else
                                    {
                                        SetIndexUpDownMaximum(0);
                                    }
                                    n = temp;
                                    param = tempParam;
                                }
                                Thread.Sleep(100);
                            }
                        });
                        
                        GroupBox.Disposed += (sender, e) =>
                        {
                            indexUpdater.Abort();
                        };

                        GroupBox.Controls.AddRange(new Control[]{
                            ListBox,
                            IndexUpDown,
                            LowerUpDown,
                            UpperUpDown,
                            LowerDeviationLabel,
                            UpperDeviationLabel,
                        });
                        parameterControls.Add(GroupBox);

                        foreach (var param in PluralFitParams())
                        {
                            var array = new Tuple<double, double>[100];
                            for(int i = 0; i < 100; i++)
                            {
                                array[i] = new Tuple<double, double>(0.0,0.0);
                            }
                            polyFits.Add(param, array);
                            getterCalls.Add(param, i =>
                            {
                                if (i < (int)IndexUpDown.Value)
                                {
                                    return polyFits[param][i].Item1 - polyFits[param][i].Item2;
                                }
                                else return Model.NULL;
                            });
                            enumsIndexers.Add(param);
                        }
                        foreach(var param in SingularFitParams())
                        {
                            singleFits.Add(param, new Tuple<double, double>(0.0,0.0));
                            getterCalls.Add(param, i =>
                            {
                                return i == 0 ? singleFits[param].Item1 - singleFits[param].Item2 : Model.NULL;
                            });
                            enumsIndexers.Add(param);
                        }

                        enumSelected = enumsIndexers[0];
                        indexUpdater.Start();
                    })();
                })();

                dimensioningPart = new LoadMenu(new LoadMenu.InitialParameters
                {
                    model = this,
                    PARAMBOX_WIDTH = StaticFields.PARAM_BOX_WIDTH,
                    parameterControls = parameterControls,
                    getterCalls = getterCalls,
                    setterCalls = setterCalls
                })
                {
                    Visible = true,
                    Enabled = true,
                    AutoSize = true,
                    Dock = DockStyle.Fill
                };

                parameterControls = new List<Control>();
                getterCalls = new Dictionary<Enum, Func<int, double>>();
                setterCalls = new Dictionary<Enum, Action<int, double>>();
                toolTip = new ToolTip();
            }
            
            return new Dictionary<UserControl, Func<string>>()
            {
                {dimensioningPart, Vocabulary.TabPagesNames.Load }
            };
        }
        protected abstract Dictionary<Enum, PredefinedGroupContainer> PredefinedGroups();
        protected abstract List<Enum> PluralFitParams();
        protected abstract List<Enum> SingularFitParams();
        protected abstract int ToleratedElementsQuantity();

        private delegate void SetValueCallBack(object value);
    }
}