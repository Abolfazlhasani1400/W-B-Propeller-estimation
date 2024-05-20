using System;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace HoltropResistance
{
    public partial class Resistance : Form
    {
        public Resistance()
        {
            InitializeComponent();
        }
        #region placeHolders
        private void RpmStepConter_Textbox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RpmStepConter_Textbox.Text))
            {
                RpmStepConter_Textbox.Text = "5";
            }
        }

        private void MinimumRPM_Textbox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MinimumRPM_Textbox.Text))
            {
                MinimumRPM_Textbox.Text = "50";
            }
        }

        private void MaximumRPM_Textbox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaximumRPM_Textbox.Text))
            {
                MaximumRPM_Textbox.Text = "1000";
            }
        }

        private void RpmStepConter_Textbox_GotFocus(object sender, EventArgs e)
        {
            if (RpmStepConter_Textbox.Text == "5")
            {
                RpmStepConter_Textbox.Text = String.Empty;
            }
        }

        private void MinimumRPM_Textbox_GotFocus(object sender, EventArgs e)
        {
            if (MinimumRPM_Textbox.Text == "50")
            {
                MinimumRPM_Textbox.Text = String.Empty;
            }
        }

        private void MaximumRPM_Textbox_GotFocus(object sender, EventArgs e)
        {
            if (MaximumRPM_Textbox.Text == "1000")
            {
                MaximumRPM_Textbox.Text = String.Empty;
            }
        }

        private void DiameterStepCounter_TextBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DiameterStepCounter_TextBox.Text))
            {
                DiameterStepCounter_TextBox.Text = "0.1";
            }
        }

        private void MinimumDiameter_TextBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MinimumDiameter_TextBox.Text))
            {
                MinimumDiameter_TextBox.Text = (T / 2).ToString();
            }
        }

        private void MaximumDiameter_Textbox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MaximumDiameter_Textbox.Text))
            {
                MaximumDiameter_Textbox.Text = (T).ToString();
            }
        }

        private void DiameterStepCounter_TextBox_GotFocus(object sender, EventArgs e)
        {
            if (DiameterStepCounter_TextBox.Text == "0.1")
            {
                DiameterStepCounter_TextBox.Text = string.Empty;
            }
        }

        private void MinimumDiameter_TextBox_GotFocus(object sender, EventArgs e)
        {
            if (MinimumDiameter_TextBox.Text == (T / 2).ToString())
            {
                MinimumDiameter_TextBox.Text = string.Empty;
            }
        }

        private void MaximumDiameter_Textbox_GotFocus(object sender, EventArgs e)
        {
            if (MaximumDiameter_Textbox.Text == (T).ToString())
            {
                MaximumDiameter_Textbox.Text = string.Empty;
            }
        }

        #endregion

        #region GlobalVaariables

        List<Propeller> PropellerList = new List<Propeller>();
        bool UserRequest = false;

        // int and double parse tryouts
        double DOUBLE_tryout;
        int INT_tryout;


        int[] PropellerBladesNumbers = { 0, 0, 0, 0, 0, 0 };

        //B-Wagenigen Kt, Kq database
        double[,] Kq_Constants = {  {0.003794,0,0,0,0},
                                    {0.008865,2,0,0,0},
                                    {-0.03224,1,1,0,0},
                                    {0.003448,0,2,0,0},
                                    {-0.04088,0,1,1,0},
                                    {-0.10801,1,1,1,0},
                                    {-0.08854,2,1,1,0},
                                    {0.188561,0,2,1,0},
                                    {-0.00371,1,0,0,1},
                                    {0.005137,0,1,0,1},
                                    {0.020945,1,1,0,1},
                                    {0.004743,2,1,0,1},
                                    {-0.00723,2,0,1,1},
                                    {0.004384,1,1,1,1},
                                    {-0.02694,0,2,1,1},
                                    {0.055808,3,0,1,0},
                                    {0.016189,0,3,1,0},
                                    {0.003181,1,3,1,0},
                                    {0.015896,0,0,2,0},
                                    {0.047173,1,0,2,0},
                                    {0.019628,3,0,2,0},
                                    {-0.05028,0,1,2,0},
                                    {-0.03006,3,1,2,0},
                                    {0.041712,2,2,2,0},
                                    {-0.03977,0,3,2,0},
                                    {-0.0035,0,6,2,0},
                                    {-0.01069,3,3,0,1},
                                    {0.001109,3,0,0,1},
                                    {-0.00031,0,6,0,1},
                                    {0.003599,3,0,1,1},
                                    {-0.00142,0,6,1,1},
                                    {-0.00384,1,0,2,1},
                                    {0.01268,0,2,2,1},
                                    {-0.00318,2,3,2,1},
                                    {0.003343,0,6,2,1},
                                    {-0.00183,1,1,0,2},
                                    {0.000112,3,2,0,2},
                                    {-0.00003,3,6,0,2},
                                    {0.00027,1,0,1,2},
                                    {0.000833,2,0,1,2},
                                    {0.001553,0,2,1,2},
                                    {0.000303,0,6,1,2},
                                    {-0.00018,0,0,2,2},
                                    {-0.00043,0,3,2,2},
                                    {0.0000869,3,3,2,2},
                                    {-0.00047,0,6,2,2},
                                    {0.0000554,1,6,2,2} };

        double[,] Kt_Constants = { {0.00880496,0,0,0,0},
                                    {-0.204554,1,0,0,0},
                                    {0.166351,0,1,0,0},
                                    {0.158114,0,2,0,0},
                                    {-0.147581,2,0,1,0},
                                    {-0.481497,1,1,1,0},
                                    {0.415437,0,2,1,0},
                                    {0.0144043,0,0,0,1},
                                    {-0.0530054,2,0,0,1},
                                    {0.0143481,0,1,0,1},
                                    {0.0606826,1,1,0,1},
                                    {-0.0125894,0,0,1,1},
                                    {0.0109689,1,0,1,1},
                                    {-0.133689,0,3,0,0},
                                    {0.00638407,0,6,0,0},
                                    {-0.00132718,2,6,0,0},
                                    {0.168496,3,0,1,0},
                                    {-0.0507214,0,0,2,0},
                                    {0.0854559,2,0,2,0},
                                    {-0.0504475,3,0,2,0},
                                    {0.010465,1,6,2,0},
                                    {-0.00648272,2,6,2,0},
                                    {-0.00841728,0,3,0,1},
                                    {0.0168424,1,3,0,1},
                                    {-0.00102296,3,3,0,1},
                                    {-0.0317791,0,3,1,1},
                                    {0.018604,1,0,2,1},
                                    {-0.00410798,0,2,2,1},
                                    {-0.000606848,0,0,0,2},
                                    {-0.0049819,1,0,0,2},
                                    {0.0025983,2,0,0,2},
                                    {-0.000560528,3,0,0,2},
                                    {-0.00163652,1,2,0,2},
                                    {-0.000328787,1,6,0,2},
                                    {0.000116502,2,6,0,2},
                                    {0.000690904,0,0,1,2},
                                    {0.00421749,0,3,1,2},
                                    {0.0000565229,3,6,1,2},
                                    {0.001465564,0,3,2,2}  };
        
        //Geometry
        double Lwl;
        double Lpp;
        double B;
        double T;
        double Displacement;
        double Lcb;
        double Abt;
        double Hb;
        double Cm;
        double Cwp;
        double At;
        double Cstern;
        double ServiceSpeed; //in knots Only
        double AdvanceSpeed;
        // appendage wetted surface    
        double rudderbehindskeg;
        double rudderbehindester;
        double twinscrewbalnce;
        double shaftbracket;
        double skeg;
        double strutbossing;
        double hullbossing;
        double shaft;
        double stablizerfin;
        double dome;
        double bilgekeel;

        //propeller properties
        double n;
        double J;
        double Thrust;
        double Torque;
        double Kt;
        double Kq;
        double Kq_min;
        double Kt_min;
        double Efficiency;
        double AreaRatio;
        double pitchRatio;
        double PropellerDiameter;
        double PropellerBladeNumber;

        double P0;
        double P_Vapor;
        double density;

        #endregion

        #region HoltropResistance
        void HoltropPrimaryResults(double service_speed)
        {
            //get the velocity from this function arrgument//
            double Velocity = service_speed * 0.5144;
            //Geometry Coefficients
            double Cb = Displacement / (Lwl * B * T);
            double Am = Cm * (B * T);
            double Awp = Cwp * (Lwl * B);
            double Cp = Displacement / (Am * Lwl);
            //Wetted Surface Area//
            double WSA = (Lwl * (B + (2.0 * T))) * (Math.Pow(Cm, 0.5)) * (0.453 + (0.4425 * Cb) - (0.2862 * Cm) - (0.003467 * (B / T)) + (0.3696 * Cwp)) + (2.38 * (Abt / Cb));
            //Seawater properties
            double g = 9.81;
            double Density = 1025.0;
            double Viscosity = 0.00101; //cengel
            double Re = (Density * Velocity * Lwl) / Viscosity;
            double LR = (1.0 - Cp + ((0.06 * Cp * Lcb) / (4.0 * Cp - 1))) * Lwl;
            double E_angle = 1.0 + 89.0 * Math.Exp(-1.0 * (Math.Pow((Lwl / B), 0.80856)) * (Math.Pow((1.0 - Cwp), 0.30484)) * (Math.Pow((1.0 - Cp - 0.0225 * Lcb), 0.6367)) * (Math.Pow((LR / B), 0.34574)) * (Math.Pow(((100.0 * Displacement) / (Math.Pow(Lwl, 3.0))), 0.16302)));
            double Fn = Velocity / Math.Pow(Lwl * g, 0.5);
            double Fni = Velocity / Math.Pow((g * (T - Hb - 0.25 * Math.Pow(Abt, 0.5)) + 0.15 * Math.Pow(Velocity, 2)), 0.5);
            double Fnt = Velocity / Math.Pow((2.0 * g * At / (B + B * Cwp)), 0.5);
            //HOLTROP STATIC VARIABLES
            double Cf = 0.075 / Math.Pow((Math.Log10(Re) - 2.0), 2.0);
            double PB = 0.56 * (Math.Pow(Abt, 0.5)) / (T - 1.5 * Hb);
            //C variable
            double c3 = (0.56 * Math.Pow(Abt, 1.5)) / (B * T * (0.31 * Math.Pow(Abt, 0.5) + T - Hb));
            double c4 = 0;
            if ((T / Lwl) == 0.04 || (T / Lwl) < 0.04)
            {
                c4 = T / Lwl;
            }
            if ((T / Lwl) > 0.04)
            {
                c4 = 0.04;
            }
            double c5 = 1 - ((0.8 * (At)) / (B * T * Cm));
            double c6 = 0;
            if (Fnt < 5)
            {
                c6 = 0.2 * (1 - 0.2 * Fnt);
            }
            if (Fnt > 5 || Fnt == 5)
            {
                c6 = 0;
            }
            double c7 = 0;
            if ((B / Lwl) < 0.11)
            {
                c7 = 0.229577 * Math.Pow((B / Lwl), 0.33333);
            }
            if ((B / Lwl) > 0.11 && (B / Lwl) < 0.25)
            {
                c7 = B / Lwl;
            }
            if ((B / Lwl) > 0.25)
            {
                c7 = 0.5 - 0.0625 * (Lwl / B);
            }
            double c12 = 0;
            if ((T / Lwl) > 0.05)
            {
                c12 = Math.Pow((T / Lwl), 0.2228446);
            }
            if ((T / Lwl) > 0.02 && (T / Lwl) < 0.05)
            {
                c12 = (48.2 * Math.Pow((T / Lwl - 0.02), 2.078)) + 0.479948;
            }
            if (T / Lwl < 0.02)
            {
                c12 = 0.479948;
            }
            double c13 = 1 + 0.003 * Cstern;
            double c15 = 0.0;
            if ((Math.Pow(Lwl, 3) / Displacement) > 1727)
            {
                c15 = 0.0;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) < 512.0)
            {
                c15 = -1.69385;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) > 512.0 && (Math.Pow(Lwl, 3.0) / Displacement) < 1727.0)
            {
                c15 = -1.69385 + ((Lwl / Math.Pow(Displacement, 0.3333)) - 8.0) / 2.36;
            }
            double c16 = 0.0;
            if (Cp < 0.8)
            {
                c16 = 8.07981 * Cp - 13.8673 * Math.Pow(Cp, 2.0) + 6.984388 * Math.Pow(Cp, 3.0);
            }
            if (Cp > 0.8)
            {
                c16 = 1.73014 - 0.7067 * Cp;
            }
            /////////////////////
            double lambda = 0;
            if ((Lwl / B) < 12.0)
            {
                lambda = 1.446 * Cp - 0.03 * (Lwl / B);
            }
            if ((Lwl / B) > 12.0)
            {
                lambda = 1.446 * Cp - 0.36;
            }
            double m1 = 0.0140407 * (Lwl / T) - (1.75254 * (Math.Pow(Displacement, 0.3333)) / Lwl) - 4.79323 * (B / Lwl) - c16;
            double m2 = c15 * Math.Pow(Cp, 2.0) * Math.Exp(-0.1 * Math.Pow(Fn, -2.0));
            double d = -0.9;
            double c1 = 2223105 * (Math.Pow(c7, 3.78613)) * (Math.Pow((T / B), 1.07961) * (Math.Pow((90 - E_angle), -1.37565)));
            double c2 = Math.Exp(-1.89 * Math.Pow(c3, 0.5));
            double Ca = 0.006 * Math.Pow((Lwl + 100.0), -0.16) - 0.00205 + 0.003 * Math.Pow((Lwl / 7.5), 0.5) * Math.Pow(Cb, 4.0) * c2 * (0.04 - c4);
           
            //
            //CALCULATING RESISTANCE
            //
            double Rtotal, Rf, Rapp, Rwave, Rb, Rtr, Ra, K;
            double Rf_per, Rapp_per, Rwave_per, Rb_per, Rtr_per, Ra_per;
           
            //K = 1 +K1
            K = c13 * (0.93 + c12 * Math.Pow((B / LR), 0.92497) * Math.Pow((0.95 - Cp), -0.521448) * Math.Pow((1 - Cp + 0.0225 * Lcb), 0.6906));
            
            //resistances
            Rf = 0.5 * Density * Cf * Math.Pow(Velocity, 2.0) * WSA;
            Rwave = c1 * c2 * c5 * Displacement * Density * g * Math.Exp((m1 * Math.Pow(Fn, d)) + (m2 * Math.Cos(lambda * Math.Pow(Fn, -2.0))));
            Rb = 0.11 * Math.Exp(-3.0 * Math.Pow(PB, -2.0)) * Math.Pow(Fni, 3) * Math.Pow(Abt, 1.5) * Density * g / (1 + Math.Pow(Fni, 2.0));
            Rtr = 0.5 * Density * Math.Pow(Velocity, 2.0) * At * c6;
            Ra = 0.5 * Density * Math.Pow(Velocity, 2.0) * WSA * Ca;
            Rapp = 0.5 * Density * Math.Pow(Velocity, 2.0) * Cf * (rudderbehindskeg * 1.75 + rudderbehindester * 1.4 + twinscrewbalnce * 2.8 + shaftbracket * 3 + skeg * 1.75 + strutbossing * 3 + hullbossing * 2 + shaft * 3 + stablizerfin * 2.8 + dome * 2.7 + bilgekeel * 1.4);
            //total resistance
            Rtotal = (Rf * K) + Rapp + Rwave + Rb + Rtr + Ra;

            //pencentage of each resistance component
            Rf_per = (Rf * K / Rtotal) * 100;
            Rapp_per = (Rapp / Rtotal) * 100;
            Rwave_per = (Rwave / Rtotal) * 100;
            Rb_per = (Rb / Rtotal) * 100;
            Rtr_per = (Rtr / Rtotal) * 100;
            Ra_per = (Ra / Rtotal) * 100;

            //Resistance value (KN):
            total.Text = (Rtotal / 1000).ToString();
            friction.Text = (Rf * K /1000).ToString();
            appendage.Text = (Rapp / 1000).ToString();
            wave.Text = (Rwave / 1000).ToString();
            bulb.Text = (Rb / 1000).ToString();
            tran.Text = (Rtr / 1000).ToString();
            ra_text.Text = (Ra / 1000).ToString();

            //Resistance Pencentage:
            fricrion_per.Text = Rf_per.ToString();
            app_per.Text = Rapp_per.ToString();
            wave_per.Text = Rwave_per.ToString();
            bulb_per.Text = Rb_per.ToString();
            transom_per.Text = Rtr_per.ToString();
            ra_per_text.Text = Ra_per.ToString();
        }

        double HoltropTotalResistance(double Velocity)
        {
            //get the velocity from arrgument//
            double V = Velocity * 0.5144;
            //Geometry Coefficients
            double Cb = Displacement / (Lwl * B * T);
            double Am = Cm * (B * T);
            double Awp = Cwp * (Lwl * B);
            double Cp = Displacement / (Am * Lwl);
            //Wetted Surface Area//
            double WSA = (Lwl * (B + (2.0 * T))) * (Math.Pow(Cm, 0.5)) * (0.453 + (0.4425 * Cb) - (0.2862 * Cm) - (0.003467 * (B / T)) + (0.3696 * Cwp)) + (2.38 * (Abt / Cb));
            //Seawater properties
            double g = 9.81;
            double Density = 1025.0;
            double Viscosity = 0.00101; //cengel
            double Re = (Density * V * Lwl) / Viscosity;
            double LR = (1.0 - Cp + ((0.06 * Cp * Lcb) / (4.0 * Cp - 1))) * Lwl;
            double E_angle = 1.0 + 89.0 * Math.Exp(-1.0 * (Math.Pow((Lwl / B), 0.80856)) * (Math.Pow((1.0 - Cwp), 0.30484)) * (Math.Pow((1.0 - Cp - 0.0225 * Lcb), 0.6367)) * (Math.Pow((LR / B), 0.34574)) * (Math.Pow(((100.0 * Displacement) / (Math.Pow(Lwl, 3.0))), 0.16302)));
            double Fn = V / Math.Pow(Lwl * g, 0.5);
            double Fni = V / Math.Pow((g * (T - Hb - 0.25 * Math.Pow(Abt, 0.5)) + 0.15 * Math.Pow(V, 2)), 0.5);
            double Fnt = V / Math.Pow((2.0 * g * At / (B + B * Cwp)), 0.5);
            //HOLTROP STATIC VARIABLES
            double Cf = 0.075 / Math.Pow((Math.Log10(Re) - 2.0), 2.0);
            double PB = 0.56 * (Math.Pow(Abt, 0.5)) / (T - 1.5 * Hb);
            //
            //C variable
            //
            double c3 = (0.56 * Math.Pow(Abt, 1.5)) / (B * T * (0.31 * Math.Pow(Abt, 0.5) + T - Hb));
            double c4 = 0;
            if ((T / Lwl) == 0.04 || (T / Lwl) < 0.04)
            {
                c4 = T / Lwl;
            }
            if ((T / Lwl) > 0.04)
            {
                c4 = 0.04;
            }
            double c5 = 1 - ((0.8 * (At)) / (B * T * Cm));
            double c6 = 0;
            if (Fnt < 5)
            {
                c6 = 0.2 * (1 - 0.2 * Fnt);
            }
            if (Fnt > 5 || Fnt == 5)
            {
                c6 = 0;
            }
            double c7 = 0;
            if ((B / Lwl) < 0.11)
            {
                c7 = 0.229577 * Math.Pow((B / Lwl), 0.33333);
            }
            if ((B / Lwl) > 0.11 && (B / Lwl) < 0.25)
            {
                c7 = B / Lwl;
            }
            if ((B / Lwl) > 0.25)
            {
                c7 = 0.5 - 0.0625 * (Lwl / B);
            }
            double c12 = 0;
            if ((T / Lwl) > 0.05)
            {
                c12 = Math.Pow((T / Lwl), 0.2228446);
            }
            if ((T / Lwl) > 0.02 && (T / Lwl) < 0.05)
            {
                c12 = (48.2 * Math.Pow((T / Lwl - 0.02), 2.078)) + 0.479948;
            }
            if (T / Lwl < 0.02)
            {
                c12 = 0.479948;
            }
            double c13 = 1 + 0.003 * Cstern;
            double c15 = 0.0;
            if ((Math.Pow(Lwl, 3) / Displacement) > 1727)
            {
                c15 = 0.0;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) < 512.0)
            {
                c15 = -1.69385;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) > 512.0 && (Math.Pow(Lwl, 3.0) / Displacement) < 1727.0)
            {
                c15 = -1.69385 + ((Lwl / Math.Pow(Displacement, 0.3333)) - 8.0) / 2.36;
            }
            double c16 = 0.0;
            if (Cp < 0.8)
            {
                c16 = 8.07981 * Cp - 13.8673 * Math.Pow(Cp, 2.0) + 6.984388 * Math.Pow(Cp, 3.0);
            }
            if (Cp > 0.8)
            {
                c16 = 1.73014 - 0.7067 * Cp;
            }
            /////////////////////
            double lambda = 0;
            if ((Lwl / B) < 12.0)
            {
                lambda = 1.446 * Cp - 0.03 * (Lwl / B);
            }
            if ((Lwl / B) > 12.0)
            {
                lambda = 1.446 * Cp - 0.36;
            }
            double m1 = 0.0140407 * (Lwl / T) - (1.75254 * (Math.Pow(Displacement, 0.3333)) / Lwl) - 4.79323 * (B / Lwl) - c16;
            double m2 = c15 * Math.Pow(Cp, 2.0) * Math.Exp(-0.1 * Math.Pow(Fn, -2.0));
            double d = -0.9;
            double c1 = 2223105 * (Math.Pow(c7, 3.78613)) * (Math.Pow((T / B), 1.07961) * (Math.Pow((90 - E_angle), -1.37565)));
            double c2 = Math.Exp(-1.89 * Math.Pow(c3, 0.5));
            double Ca = 0.006 * Math.Pow((Lwl + 100.0), -0.16) - 0.00205 + 0.003 * Math.Pow((Lwl / 7.5), 0.5) * Math.Pow(Cb, 4.0) * c2 * (0.04 - c4);
            //
            //CALCULATING RESISTANCE
            //
            double TotalResistance, FrictionalResistance, AppendageResistance, WaveResistance, Rb, TransomSternResistance, Ra, K;

            K = c13 * (0.93 + c12 * Math.Pow((B / LR), 0.92497) * Math.Pow((0.95 - Cp), -0.521448) * Math.Pow((1 - Cp + 0.0225 * Lcb), 0.6906)); ///K = 1 +K1
            //resistance calculation
            FrictionalResistance = 0.5 * Density * Cf * Math.Pow(V, 2.0) * WSA;
            WaveResistance = c1 * c2 * c5 * Displacement * Density * g * Math.Exp((m1 * Math.Pow(Fn, d)) + (m2 * Math.Cos(lambda * Math.Pow(Fn, -2.0))));
            Rb = 0.11 * Math.Exp(-3.0 * Math.Pow(PB, -2.0)) * Math.Pow(Fni, 3) * Math.Pow(Abt, 1.5) * Density * g / (1 + Math.Pow(Fni, 2.0));
            TransomSternResistance = 0.5 * Density * Math.Pow(V, 2.0) * At * c6;
            Ra = 0.5 * Density * Math.Pow(V, 2.0) * WSA * Ca;
            AppendageResistance = 0.5 * Density * Math.Pow(V, 2.0) * Cf * (rudderbehindskeg * 1.75 + rudderbehindester * 1.4 + twinscrewbalnce * 2.8 + shaftbracket * 3 + skeg * 1.75 + strutbossing * 3 + hullbossing * 2 + shaft * 3 + stablizerfin * 2.8 + dome * 2.7 + bilgekeel * 1.4);
            //total resistance
            TotalResistance = (FrictionalResistance * K) + AppendageResistance + WaveResistance + Rb + TransomSternResistance + Ra;

            return TotalResistance;
        }
        double HoltropFrictionalResistance(double Velocity)
        {
            //get the velocity from arrgument//
            double V = Velocity * 0.5144;
            //Geometry Coefficients
            double Cb = Displacement / (Lwl * B * T);
            double Am = Cm * (B * T);
            double Awp = Cwp * (Lwl * B);
            double Cp = Displacement / (Am * Lwl);
            //Wetted Surface Area//
            double WSA = (Lwl * (B + (2.0 * T))) * (Math.Pow(Cm, 0.5)) * (0.453 + (0.4425 * Cb) - (0.2862 * Cm) - (0.003467 * (B / T)) + (0.3696 * Cwp)) + (2.38 * (Abt / Cb));
            //Seawater properties
            double g = 9.81;
            double Density = 1025.0;
            double Viscosity = 0.00101; //cengel
            double Re = (Density * V * Lwl) / Viscosity;
            double LR = (1.0 - Cp + ((0.06 * Cp * Lcb) / (4.0 * Cp - 1))) * Lwl;
            double E_angle = 1.0 + 89.0 * Math.Exp(-1.0 * (Math.Pow((Lwl / B), 0.80856)) * (Math.Pow((1.0 - Cwp), 0.30484)) * (Math.Pow((1.0 - Cp - 0.0225 * Lcb), 0.6367)) * (Math.Pow((LR / B), 0.34574)) * (Math.Pow(((100.0 * Displacement) / (Math.Pow(Lwl, 3.0))), 0.16302)));
            double Fn = V / Math.Pow(Lwl * g, 0.5);
            double Fni = V / Math.Pow((g * (T - Hb - 0.25 * Math.Pow(Abt, 0.5)) + 0.15 * Math.Pow(V, 2)), 0.5);
            double Fnt = V / Math.Pow((2.0 * g * At / (B + B * Cwp)), 0.5);
            //HOLTROP STATIC VARIABLES
            double Cf = 0.075 / Math.Pow((Math.Log10(Re) - 2.0), 2.0);
            double PB = 0.56 * (Math.Pow(Abt, 0.5)) / (T - 1.5 * Hb);
            //
            //C variable
            //
            double c3 = (0.56 * Math.Pow(Abt, 1.5)) / (B * T * (0.31 * Math.Pow(Abt, 0.5) + T - Hb));
            double c4 = 0;
            if ((T / Lwl) == 0.04 || (T / Lwl) < 0.04)
            {
                c4 = T / Lwl;
            }
            if ((T / Lwl) > 0.04)
            {
                c4 = 0.04;
            }
            double c5 = 1 - ((0.8 * (At)) / (B * T * Cm));
            double c6 = 0;
            if (Fnt < 5)
            {
                c6 = 0.2 * (1 - 0.2 * Fnt);
            }
            if (Fnt > 5 || Fnt == 5)
            {
                c6 = 0;
            }
            double c7 = 0;
            if ((B / Lwl) < 0.11)
            {
                c7 = 0.229577 * Math.Pow((B / Lwl), 0.33333);
            }
            if ((B / Lwl) > 0.11 && (B / Lwl) < 0.25)
            {
                c7 = B / Lwl;
            }
            if ((B / Lwl) > 0.25)
            {
                c7 = 0.5 - 0.0625 * (Lwl / B);
            }
            double c12 = 0;
            if ((T / Lwl) > 0.05)
            {
                c12 = Math.Pow((T / Lwl), 0.2228446);
            }
            if ((T / Lwl) > 0.02 && (T / Lwl) < 0.05)
            {
                c12 = (48.2 * Math.Pow((T / Lwl - 0.02), 2.078)) + 0.479948;
            }
            if (T / Lwl < 0.02)
            {
                c12 = 0.479948;
            }
            double c13 = 1 + 0.003 * Cstern;
            double c15 = 0.0;
            if ((Math.Pow(Lwl, 3) / Displacement) > 1727)
            {
                c15 = 0.0;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) < 512.0)
            {
                c15 = -1.69385;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) > 512.0 && (Math.Pow(Lwl, 3.0) / Displacement) < 1727.0)
            {
                c15 = -1.69385 + ((Lwl / Math.Pow(Displacement, 0.3333)) - 8.0) / 2.36;
            }
            double c16 = 0.0;
            if (Cp < 0.8)
            {
                c16 = 8.07981 * Cp - 13.8673 * Math.Pow(Cp, 2.0) + 6.984388 * Math.Pow(Cp, 3.0);
            }
            if (Cp > 0.8)
            {
                c16 = 1.73014 - 0.7067 * Cp;
            }
            /////////////////////
            double lambda = 0;
            if ((Lwl / B) < 12.0)
            {
                lambda = 1.446 * Cp - 0.03 * (Lwl / B);
            }
            if ((Lwl / B) > 12.0)
            {
                lambda = 1.446 * Cp - 0.36;
            }
            double m1 = 0.0140407 * (Lwl / T) - (1.75254 * (Math.Pow(Displacement, 0.3333)) / Lwl) - 4.79323 * (B / Lwl) - c16;
            double m2 = c15 * Math.Pow(Cp, 2.0) * Math.Exp(-0.1 * Math.Pow(Fn, -2.0));
            double d = -0.9;
            double c1 = 2223105 * (Math.Pow(c7, 3.78613)) * (Math.Pow((T / B), 1.07961) * (Math.Pow((90 - E_angle), -1.37565)));
            double c2 = Math.Exp(-1.89 * Math.Pow(c3, 0.5));
            double Ca = 0.006 * Math.Pow((Lwl + 100.0), -0.16) - 0.00205 + 0.003 * Math.Pow((Lwl / 7.5), 0.5) * Math.Pow(Cb, 4.0) * c2 * (0.04 - c4);
            //
            //CALCULATING RESISTANCE
            //
            double TotalResistance, FrictionalResistance, AppendageResistance, WaveResistance, Rb, TransomSternResistance, Ra, K;

            K = c13 * (0.93 + c12 * Math.Pow((B / LR), 0.92497) * Math.Pow((0.95 - Cp), -0.521448) * Math.Pow((1 - Cp + 0.0225 * Lcb), 0.6906)); ///K = 1 +K1
            //resistance calculation
            FrictionalResistance = 0.5 * Density * Cf * Math.Pow(V, 2.0) * WSA;
            WaveResistance = c1 * c2 * c5 * Displacement * Density * g * Math.Exp((m1 * Math.Pow(Fn, d)) + (m2 * Math.Cos(lambda * Math.Pow(Fn, -2.0))));
            Rb = 0.11 * Math.Exp(-3.0 * Math.Pow(PB, -2.0)) * Math.Pow(Fni, 3) * Math.Pow(Abt, 1.5) * Density * g / (1 + Math.Pow(Fni, 2.0));
            TransomSternResistance = 0.5 * Density * Math.Pow(V, 2.0) * At * c6;
            Ra = 0.5 * Density * Math.Pow(V, 2.0) * WSA * Ca;
            AppendageResistance = 0.5 * Density * Math.Pow(V, 2.0) * Cf * (rudderbehindskeg * 1.75 + rudderbehindester * 1.4 + twinscrewbalnce * 2.8 + shaftbracket * 3 + skeg * 1.75 + strutbossing * 3 + hullbossing * 2 + shaft * 3 + stablizerfin * 2.8 + dome * 2.7 + bilgekeel * 1.4);
            //total resistance
            TotalResistance = (FrictionalResistance * K) + AppendageResistance + WaveResistance + Rb + TransomSternResistance + Ra;

            return FrictionalResistance;
        }
        double HoltropWaveResistance(double Velocity)
        {
            //get the velocity from arrgument//
            double V = Velocity * 0.5144;
            //Geometry Coefficients
            double Cb = Displacement / (Lwl * B * T);
            double Am = Cm * (B * T);
            double Awp = Cwp * (Lwl * B);
            double Cp = Displacement / (Am * Lwl);
            //Wetted Surface Area//
            double WSA = (Lwl * (B + (2.0 * T))) * (Math.Pow(Cm, 0.5)) * (0.453 + (0.4425 * Cb) - (0.2862 * Cm) - (0.003467 * (B / T)) + (0.3696 * Cwp)) + (2.38 * (Abt / Cb));
            //Seawater properties
            double g = 9.81;
            double Density = 1025.0;
            double Viscosity = 0.00101; //cengel
            double Re = (Density * V * Lwl) / Viscosity;
            double LR = (1.0 - Cp + ((0.06 * Cp * Lcb) / (4.0 * Cp - 1))) * Lwl;
            double E_angle = 1.0 + 89.0 * Math.Exp(-1.0 * (Math.Pow((Lwl / B), 0.80856)) * (Math.Pow((1.0 - Cwp), 0.30484)) * (Math.Pow((1.0 - Cp - 0.0225 * Lcb), 0.6367)) * (Math.Pow((LR / B), 0.34574)) * (Math.Pow(((100.0 * Displacement) / (Math.Pow(Lwl, 3.0))), 0.16302)));
            double Fn = V / Math.Pow(Lwl * g, 0.5);
            double Fni = V / Math.Pow((g * (T - Hb - 0.25 * Math.Pow(Abt, 0.5)) + 0.15 * Math.Pow(V, 2)), 0.5);
            double Fnt = V / Math.Pow((2.0 * g * At / (B + B * Cwp)), 0.5);
            //HOLTROP STATIC VARIABLES
            double Cf = 0.075 / Math.Pow((Math.Log10(Re) - 2.0), 2.0);
            double PB = 0.56 * (Math.Pow(Abt, 0.5)) / (T - 1.5 * Hb);
            //
            //C variable
            //
            double c3 = (0.56 * Math.Pow(Abt, 1.5)) / (B * T * (0.31 * Math.Pow(Abt, 0.5) + T - Hb));
            double c4 = 0;
            if ((T / Lwl) == 0.04 || (T / Lwl) < 0.04)
            {
                c4 = T / Lwl;
            }
            if ((T / Lwl) > 0.04)
            {
                c4 = 0.04;
            }
            double c5 = 1 - ((0.8 * (At)) / (B * T * Cm));
            double c6 = 0;
            if (Fnt < 5)
            {
                c6 = 0.2 * (1 - 0.2 * Fnt);
            }
            if (Fnt > 5 || Fnt == 5)
            {
                c6 = 0;
            }
            double c7 = 0;
            if ((B / Lwl) < 0.11)
            {
                c7 = 0.229577 * Math.Pow((B / Lwl), 0.33333);
            }
            if ((B / Lwl) > 0.11 && (B / Lwl) < 0.25)
            {
                c7 = B / Lwl;
            }
            if ((B / Lwl) > 0.25)
            {
                c7 = 0.5 - 0.0625 * (Lwl / B);
            }
            double c12 = 0;
            if ((T / Lwl) > 0.05)
            {
                c12 = Math.Pow((T / Lwl), 0.2228446);
            }
            if ((T / Lwl) > 0.02 && (T / Lwl) < 0.05)
            {
                c12 = (48.2 * Math.Pow((T / Lwl - 0.02), 2.078)) + 0.479948;
            }
            if (T / Lwl < 0.02)
            {
                c12 = 0.479948;
            }
            double c13 = 1 + 0.003 * Cstern;
            double c15 = 0.0;
            if ((Math.Pow(Lwl, 3) / Displacement) > 1727)
            {
                c15 = 0.0;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) < 512.0)
            {
                c15 = -1.69385;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) > 512.0 && (Math.Pow(Lwl, 3.0) / Displacement) < 1727.0)
            {
                c15 = -1.69385 + ((Lwl / Math.Pow(Displacement, 0.3333)) - 8.0) / 2.36;
            }
            double c16 = 0.0;
            if (Cp < 0.8)
            {
                c16 = 8.07981 * Cp - 13.8673 * Math.Pow(Cp, 2.0) + 6.984388 * Math.Pow(Cp, 3.0);
            }
            if (Cp > 0.8)
            {
                c16 = 1.73014 - 0.7067 * Cp;
            }
            /////////////////////
            double lambda = 0;
            if ((Lwl / B) < 12.0)
            {
                lambda = 1.446 * Cp - 0.03 * (Lwl / B);
            }
            if ((Lwl / B) > 12.0)
            {
                lambda = 1.446 * Cp - 0.36;
            }
            double m1 = 0.0140407 * (Lwl / T) - (1.75254 * (Math.Pow(Displacement, 0.3333)) / Lwl) - 4.79323 * (B / Lwl) - c16;
            double m2 = c15 * Math.Pow(Cp, 2.0) * Math.Exp(-0.1 * Math.Pow(Fn, -2.0));
            double d = -0.9;
            double c1 = 2223105 * (Math.Pow(c7, 3.78613)) * (Math.Pow((T / B), 1.07961) * (Math.Pow((90 - E_angle), -1.37565)));
            double c2 = Math.Exp(-1.89 * Math.Pow(c3, 0.5));
            double Ca = 0.006 * Math.Pow((Lwl + 100.0), -0.16) - 0.00205 + 0.003 * Math.Pow((Lwl / 7.5), 0.5) * Math.Pow(Cb, 4.0) * c2 * (0.04 - c4);
            //
            //CALCULATING RESISTANCE
            //
            double TotalResistance, FrictionalResistance, AppendageResistance, WaveResistance, Rb, TransomSternResistance, Ra, K;

            K = c13 * (0.93 + c12 * Math.Pow((B / LR), 0.92497) * Math.Pow((0.95 - Cp), -0.521448) * Math.Pow((1 - Cp + 0.0225 * Lcb), 0.6906)); ///K = 1 +K1
            //resistance calculation
            FrictionalResistance = 0.5 * Density * Cf * Math.Pow(V, 2.0) * WSA;
            WaveResistance = c1 * c2 * c5 * Displacement * Density * g * Math.Exp((m1 * Math.Pow(Fn, d)) + (m2 * Math.Cos(lambda * Math.Pow(Fn, -2.0))));
            Rb = 0.11 * Math.Exp(-3.0 * Math.Pow(PB, -2.0)) * Math.Pow(Fni, 3) * Math.Pow(Abt, 1.5) * Density * g / (1 + Math.Pow(Fni, 2.0));
            TransomSternResistance = 0.5 * Density * Math.Pow(V, 2.0) * At * c6;
            Ra = 0.5 * Density * Math.Pow(V, 2.0) * WSA * Ca;
            AppendageResistance = 0.5 * Density * Math.Pow(V, 2.0) * Cf * (rudderbehindskeg * 1.75 + rudderbehindester * 1.4 + twinscrewbalnce * 2.8 + shaftbracket * 3 + skeg * 1.75 + strutbossing * 3 + hullbossing * 2 + shaft * 3 + stablizerfin * 2.8 + dome * 2.7 + bilgekeel * 1.4);
            //total resistance
            TotalResistance = (FrictionalResistance * K) + AppendageResistance + WaveResistance + Rb + TransomSternResistance + Ra;

            return WaveResistance;
        }

        double WakeFraction(double Velocity , double PropellerDiameter)
        {
            //get the velocity from this function arrgument//
            double V = Velocity * 0.5144;
            //Geometry Coefficients
            double Cb = Displacement / (Lwl * B * T);
            double Am = Cm * (B * T);
            double Awp = Cwp * (Lwl * B);
            double Cp = Displacement / (Am * Lwl);
            //Wetted Surface Area//
            double WSA = (Lwl * (B + (2.0 * T))) * (Math.Pow(Cm, 0.5)) * (0.453 + (0.4425 * Cb) - (0.2862 * Cm) - (0.003467 * (B / T)) + (0.3696 * Cwp)) + (2.38 * (Abt / Cb));
            //Seawater properties
            double g = 9.81;
            double Density = 1025.0;
            double Viscosity = 0.00101; //cengel
            double Re = (Density * V * Lwl) / Viscosity;
            double LR = (1.0 - Cp + ((0.06 * Cp * Lcb) / (4.0 * Cp - 1))) * Lwl;
            double E_angle = 1.0 + 89.0 * Math.Exp(-1.0 * (Math.Pow((Lwl / B), 0.80856)) * (Math.Pow((1.0 - Cwp), 0.30484)) * (Math.Pow((1.0 - Cp - 0.0225 * Lcb), 0.6367)) * (Math.Pow((LR / B), 0.34574)) * (Math.Pow(((100.0 * Displacement) / (Math.Pow(Lwl, 3.0))), 0.16302)));
            double Fn = V / Math.Pow(Lwl * g, 0.5);
            double Fni = V / Math.Pow((g * (T - Hb - 0.25 * Math.Pow(Abt, 0.5)) + 0.15 * Math.Pow(V, 2)), 0.5);
            double Fnt = V / Math.Pow((2.0 * g * At / (B + B * Cwp)), 0.5);
            //HOLTROP STATIC VARIABLES
            double Cf = 0.075 / Math.Pow((Math.Log10(Re) - 2.0), 2.0);
            double PB = 0.56 * (Math.Pow(Abt, 0.5)) / (T - 1.5 * Hb);
            //
            //C variable
            //
            double c3 = (0.56 * Math.Pow(Abt, 1.5)) / (B * T * (0.31 * Math.Pow(Abt, 0.5) + T - Hb));
            double c4 = 0;
            if ((T / Lwl) == 0.04 || (T / Lwl) < 0.04)
            {
                c4 = T / Lwl;
            }
            if ((T / Lwl) > 0.04)
            {
                c4 = 0.04;
            }
            double c5 = 1 - ((0.8 * (At)) / (B * T * Cm));
            double c6 = 0;
            if (Fnt < 5)
            {
                c6 = 0.2 * (1 - 0.2 * Fnt);
            }
            if (Fnt > 5 || Fnt == 5)
            {
                c6 = 0;
            }
            double c7 = 0;
            if ((B / Lwl) < 0.11)
            {
                c7 = 0.229577 * Math.Pow((B / Lwl), 0.33333);
            }
            if ((B / Lwl) > 0.11 && (B / Lwl) < 0.25)
            {
                c7 = B / Lwl;
            }
            if ((B / Lwl) > 0.25)
            {
                c7 = 0.5 - 0.0625 * (Lwl / B);
            }
            double c12 = 0;
            if ((T / Lwl) > 0.05)
            {
                c12 = Math.Pow((T / Lwl), 0.2228446);
            }
            if ((T / Lwl) > 0.02 && (T / Lwl) < 0.05)
            {
                c12 = (48.2 * Math.Pow((T / Lwl - 0.02), 2.078)) + 0.479948;
            }
            if (T / Lwl < 0.02)
            {
                c12 = 0.479948;
            }
            double c13 = 1 + 0.003 * Cstern;
            double c15 = 0.0;
            if ((Math.Pow(Lwl, 3) / Displacement) > 1727)
            {
                c15 = 0.0;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) < 512.0)
            {
                c15 = -1.69385;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) > 512.0 && (Math.Pow(Lwl, 3.0) / Displacement) < 1727.0)
            {
                c15 = -1.69385 + ((Lwl / Math.Pow(Displacement, 0.3333)) - 8.0) / 2.36;
            }
            double c16 = 0.0;
            if (Cp < 0.8)
            {
                c16 = 8.07981 * Cp - 13.8673 * Math.Pow(Cp, 2.0) + 6.984388 * Math.Pow(Cp, 3.0);
            }
            if (Cp > 0.8)
            {
                c16 = 1.73014 - 0.7067 * Cp;
            }
            /////////////////////
            double lambda = 0;
            if ((Lwl / B) < 12.0)
            {
                lambda = 1.446 * Cp - 0.03 * (Lwl / B);
            }
            if ((Lwl / B) > 12.0)
            {
                lambda = 1.446 * Cp - 0.36;
            }
            double m1 = 0.0140407 * (Lwl / T) - (1.75254 * (Math.Pow(Displacement, 0.3333)) / Lwl) - 4.79323 * (B / Lwl) - c16;
            double m2 = c15 * Math.Pow(Cp, 2.0) * Math.Exp(-0.1 * Math.Pow(Fn, -2.0));
            double d = -0.9;
            double c1 = 2223105 * (Math.Pow(c7, 3.78613)) * (Math.Pow((T / B), 1.07961) * (Math.Pow((90 - E_angle), -1.37565)));
            double c2 = Math.Exp(-1.89 * Math.Pow(c3, 0.5));
            double Ca = 0.006 * Math.Pow((Lwl + 100.0), -0.16) - 0.00205 + 0.003 * Math.Pow((Lwl / 7.5), 0.5) * Math.Pow(Cb, 4.0) * c2 * (0.04 - c4);
            //K = 1 + K1
            double K = c13 * (0.93 + c12 * Math.Pow((B / LR), 0.92497) * Math.Pow((0.95 - Cp), -0.521448) * Math.Pow((1 - Cp + 0.0225 * Lcb), 0.6906));

            double Cv = K * Cf + Ca;
            double Cp1 = 1.45 * Cp - 0.315 - 0.0225 * Lcb;

            double c8 = 0;
            if (B / T < 5)
            {
                c8 = B * WSA / (Lwl * PropellerDiameter * T);
            }
            if (B / T > 5)
            {
                c8 = WSA * (7 * B / T - 25) / (Lwl * PropellerDiameter * (B / T - 3));
            }

            double c9 = 0;
            if (c8 < 28)
            {
                c9 = c8;
            }
            if (c8 > 28)
            {
                c9 = 32 - 16 / (c8 - 24);
            }

            double c11 = 0;
            if (T / PropellerDiameter < 2)
            {
                c11 = T / PropellerDiameter;
            }
            if (T / PropellerDiameter > 2)
            {
                c11 = 0.833333 * Math.Pow((T / PropellerDiameter), 3) + 1.33333;
            }

            double WakeFraction = c9 * Cv * (Lwl / T) * (0.0661875 + 1.21756 * c11 * Cv / (1 - Cp1)) + 0.24558 * Math.Pow((B / (Lwl * (1 - Cp1))), 0.5) - 0.09726 / (0.95 - Cp) + 0.11434 / (0.95 - Cb) + 0.75 * Cstern * Cv + 0.002 * Cstern;

            return WakeFraction;
        }
        double ThrustDeduction(double Velocity)
        {
            //get the velocity from this function arrgument//
            double V = Velocity * 0.5144;
            //Geometry Coefficients
            double Cb = Displacement / (Lwl * B * T);
            double Am = Cm * (B * T);
            double Awp = Cwp * (Lwl * B);
            double Cp = Displacement / (Am * Lwl);
            //Wetted Surface Area//
            double WSA = (Lwl * (B + (2.0 * T))) * (Math.Pow(Cm, 0.5)) * (0.453 + (0.4425 * Cb) - (0.2862 * Cm) - (0.003467 * (B / T)) + (0.3696 * Cwp)) + (2.38 * (Abt / Cb));
            //Seawater properties
            double g = 9.81;
            double Density = 1025.0;
            double Viscosity = 0.00101; //cengel
            double Re = (Density * V * Lwl) / Viscosity;
            double LR = (1.0 - Cp + ((0.06 * Cp * Lcb) / (4.0 * Cp - 1))) * Lwl;
            double E_angle = 1.0 + 89.0 * Math.Exp(-1.0 * (Math.Pow((Lwl / B), 0.80856)) * (Math.Pow((1.0 - Cwp), 0.30484)) * (Math.Pow((1.0 - Cp - 0.0225 * Lcb), 0.6367)) * (Math.Pow((LR / B), 0.34574)) * (Math.Pow(((100.0 * Displacement) / (Math.Pow(Lwl, 3.0))), 0.16302)));
            double Fn = V / Math.Pow(Lwl * g, 0.5);
            double Fni = V / Math.Pow((g * (T - Hb - 0.25 * Math.Pow(Abt, 0.5)) + 0.15 * Math.Pow(V, 2)), 0.5);
            double Fnt = V / Math.Pow((2.0 * g * At / (B + B * Cwp)), 0.5);
            //HOLTROP STATIC VARIABLES
            double Cf = 0.075 / Math.Pow((Math.Log10(Re) - 2.0), 2.0);
            double PB = 0.56 * (Math.Pow(Abt, 0.5)) / (T - 1.5 * Hb);
            //
            //C variable
            //
            double c3 = (0.56 * Math.Pow(Abt, 1.5)) / (B * T * (0.31 * Math.Pow(Abt, 0.5) + T - Hb));
            double c4 = 0;
            if ((T / Lwl) == 0.04 || (T / Lwl) < 0.04)
            {
                c4 = T / Lwl;
            }
            if ((T / Lwl) > 0.04)
            {
                c4 = 0.04;
            }
            double c5 = 1 - ((0.8 * (At)) / (B * T * Cm));
            double c6 = 0;
            if (Fnt < 5)
            {
                c6 = 0.2 * (1 - 0.2 * Fnt);
            }
            if (Fnt > 5 || Fnt == 5)
            {
                c6 = 0;
            }
            double c7 = 0;
            if ((B / Lwl) < 0.11)
            {
                c7 = 0.229577 * Math.Pow((B / Lwl), 0.33333);
            }
            if ((B / Lwl) > 0.11 && (B / Lwl) < 0.25)
            {
                c7 = B / Lwl;
            }
            if ((B / Lwl) > 0.25)
            {
                c7 = 0.5 - 0.0625 * (Lwl / B);
            }
            double c12 = 0;
            if ((T / Lwl) > 0.05)
            {
                c12 = Math.Pow((T / Lwl), 0.2228446);
            }
            if ((T / Lwl) > 0.02 && (T / Lwl) < 0.05)
            {
                c12 = (48.2 * Math.Pow((T / Lwl - 0.02), 2.078)) + 0.479948;
            }
            if (T / Lwl < 0.02)
            {
                c12 = 0.479948;
            }
            double c13 = 1 + 0.003 * Cstern;
            double c15 = 0.0;
            if ((Math.Pow(Lwl, 3) / Displacement) > 1727)
            {
                c15 = 0.0;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) < 512.0)
            {
                c15 = -1.69385;
            }
            if ((Math.Pow(Lwl, 3.0) / Displacement) > 512.0 && (Math.Pow(Lwl, 3.0) / Displacement) < 1727.0)
            {
                c15 = -1.69385 + ((Lwl / Math.Pow(Displacement, 0.3333)) - 8.0) / 2.36;
            }
            double c16 = 0.0;
            if (Cp < 0.8)
            {
                c16 = 8.07981 * Cp - 13.8673 * Math.Pow(Cp, 2.0) + 6.984388 * Math.Pow(Cp, 3.0);
            }
            if (Cp > 0.8)
            {
                c16 = 1.73014 - 0.7067 * Cp;
            }
            /////////////////////
            double lambda = 0;
            if ((Lwl / B) < 12.0)
            {
                lambda = 1.446 * Cp - 0.03 * (Lwl / B);
            }
            if ((Lwl / B) > 12.0)
            {
                lambda = 1.446 * Cp - 0.36;
            }
            double m1 = 0.0140407 * (Lwl / T) - (1.75254 * (Math.Pow(Displacement, 0.3333)) / Lwl) - 4.79323 * (B / Lwl) - c16;
            double m2 = c15 * Math.Pow(Cp, 2.0) * Math.Exp(-0.1 * Math.Pow(Fn, -2.0));
            double d = -0.9;
            double c1 = 2223105 * (Math.Pow(c7, 3.78613)) * (Math.Pow((T / B), 1.07961) * (Math.Pow((90 - E_angle), -1.37565)));
            double c2 = Math.Exp(-1.89 * Math.Pow(c3, 0.5));
            double Ca = 0.006 * Math.Pow((Lwl + 100.0), -0.16) - 0.00205 + 0.003 * Math.Pow((Lwl / 7.5), 0.5) * Math.Pow(Cb, 4.0) * c2 * (0.04 - c4);
            //K = 1 + K1
            double K = c13 * (0.93 + c12 * Math.Pow((B / LR), 0.92497) * Math.Pow((0.95 - Cp), -0.521448) * Math.Pow((1 - Cp + 0.0225 * Lcb), 0.6906));

            double Cv = K * Cf + Ca;
            double Cp1 = 1.45 * Cp - 0.315 - 0.0225 * Lcb;

            double c10 = 0;
            if (Lwl / B > 5.2)
            {
                c10 = B / Lwl;
            }
            if (Lwl / B < 5.2)
            {
                c10 = 0.25 - 0.003328402 / (B / Lwl - 0.134615385);
            }
            double t = 0.001979 * Lwl / (B - B * Cp1) + 1.0585 * c10 - 0.00524 - 0.1418 * Math.Pow(PropellerDiameter, 2) / (B * T) + 0.0015 * Cstern;
            return t;
        }
        #endregion

        #region Charts
        void HoltropResistanceDiagrams(double servicespeed)
        {
            ResistancePlot.Series.Clear();
            double MaxResistance = HoltropTotalResistance(servicespeed);

            var chart = ResistancePlot.ChartAreas[0];
            chart.AxisX.IntervalType = DateTimeIntervalType.Number;

            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";

            chart.AxisX.Minimum = 0;
            chart.AxisX.Maximum = servicespeed;

            chart.AxisY.Minimum = 0;
            chart.AxisY.Maximum = MaxResistance;

            chart.AxisX.Interval = 1;
            chart.AxisY.Interval = 100000;

            ResistancePlot.Series.Add("Total resistance");
            ResistancePlot.Series["Total resistance"].ChartType = SeriesChartType.Spline;
            ResistancePlot.Series["Total resistance"].Color = Color.DarkBlue;
            ResistancePlot.Series["Total resistance"].IsVisibleInLegend = true;

            ResistancePlot.Series.Add("Frictional resistance");
            ResistancePlot.Series["Frictional resistance"].ChartType = SeriesChartType.Spline;
            ResistancePlot.Series["Frictional resistance"].Color = Color.Blue;
            ResistancePlot.Series["Frictional resistance"].IsVisibleInLegend = true;

            ResistancePlot.Series.Add("Wave resistance");
            ResistancePlot.Series["Wave resistance"].ChartType = SeriesChartType.Spline;
            ResistancePlot.Series["Wave resistance"].Color = Color.LightBlue;
            ResistancePlot.Series["Wave resistance"].IsVisibleInLegend = true;

            for (double i = 0; i < servicespeed; i += 0.01)
            {
                ResistancePlot.Series["Total resistance"].Points.AddXY(i, HoltropTotalResistance(i));
                ResistancePlot.Series["Frictional resistance"].Points.AddXY(i, HoltropFrictionalResistance(i));
                ResistancePlot.Series["Wave resistance"].Points.AddXY(i, HoltropWaveResistance(i));
            }
        }
        void SaveAsExcel()
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Execl|*.xlsx|All Files|*.*";
                saveFileDialog.Title = "Export Charts";
                saveFileDialog.ShowDialog();

                Microsoft.Office.Interop.Excel.Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
                ExcelApp.Application.Workbooks.Add(Type.Missing);
                ExcelApp.Columns.ColumnWidth = 30;

                //First Row:: Headers
                ExcelApp.Cells[1, 1] = "Speed (Knots)";
                ExcelApp.Cells[1, 2] = "Frictional Resistqance";
                ExcelApp.Cells[1, 4] = "Total Resistqance";
                ExcelApp.Cells[1, 3] = "Wave Resistqance";

                for (int i = 1; i < ServiceSpeed || i == ServiceSpeed; i++)
                {
                    //speed column
                    ExcelApp.Cells[i + 2, 1] = i;

                    //Frictional resistance column
                    ExcelApp.Cells[i + 2, 2] = HoltropFrictionalResistance(i);

                    //Wave resistance column
                    ExcelApp.Cells[i + 2, 3] = HoltropWaveResistance(i);

                    //Total resistance column
                    ExcelApp.Cells[i + 2, 4] = HoltropTotalResistance(i);
                }

                ExcelApp.ActiveWorkbook.SaveCopyAs(saveFileDialog.FileName);
                ExcelApp.ActiveWorkbook.Saved = true;
                ExcelApp.Quit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cancelled Operation");
                //this.Close();
            }
        }
        #endregion

        #region B_wagenigen Coefficient calculations
        //calculation Kt, Kq//
        double Kq_calculator(double J, double P, double A, double Z)
        {
            double sum = 0;
            for(int i=0; i < Kq_Constants.GetLength(0); i++)
            {
                sum += Kq_Constants[i, 0] * Math.Pow(J, Kq_Constants[i, 1]) * Math.Pow(P, Kq_Constants[i, 2]) * Math.Pow(A, Kq_Constants[i, 3]) * Math.Pow(Z, Kq_Constants[i, 4]);
            }
            return sum;
        }

        double Kt_calculator(double J, double P, double A, double Z)
        {
            double sum = 0;
            for (int i = 0; i < Kt_Constants.GetLength(0) ; i++)
            {
                sum += Kt_Constants[i, 0] * Math.Pow(J, Kt_Constants[i, 1]) * Math.Pow(P, Kt_Constants[i, 2]) * Math.Pow(A, Kt_Constants[i, 3]) * Math.Pow(Z, Kt_Constants[i, 4]);
            }
            return sum;
        }
        #endregion

        #region UI
        void Search_B_W(double service_speed, double MaxD, double MinD, double StepD, double MaxRPM, double MinRPM, double StepRPM,int[] BladesNumber)
        {
            //Clearing propellerList!
            PropellerList.Clear();
            //Get requierd Thrust and AdvanceSpeed From Holtrop_Resistance
            Thrust = HoltropTotalResistance(service_speed) / (1 - ThrustDeduction(service_speed));
            //Thrust = 500;
            T = 0.1;
            if (Thrust == 0 || T == 0 || service_speed == 0)
            {
                MessageBox.Show("Please Calculate Resistance! ");
                return;
            }

            density = 1025;
            P0 = density * 9.81 * T + 101325; // rho*g*h + AtmosphericPressure
            P_Vapor = 1705.1; // water vapor pressur @ 15℃ = 1.7051 KN/m^2

            //Search algorithem
            for (int blade_iterator = 0; blade_iterator < 6; blade_iterator++)// Z:Number of PropellerBlades
            {
                int Z = BladesNumber[blade_iterator];
                if (Z == 0)
                    continue;
                for (double n = MinRPM; n <= MaxRPM; n += StepRPM) // n: propeller RPM
                {
                    for (double D = MinD; D <= MaxD; D += StepD) //D: Propeller Diameter
                    {
                        AdvanceSpeed = service_speed * 0.5144 * (1 - WakeFraction(service_speed, D));

                        J = AdvanceSpeed / ((n / 60) * D);

                        if (J < 1.6 && J >= 0.2) // calculates for 1.6 < j <= 0.2
                        {
                            for (pitchRatio = 0.5; pitchRatio < 1.5; pitchRatio += 0.1)
                            {
                                //Calculate AreaRatio for each PitchRatio
                                AreaRatio = (0.692 * Thrust) / (((Math.Pow(D, 2)) * Math.Pow((P0 - P_Vapor), 0.75) * Math.Pow(AdvanceSpeed, 0.5) * (1.067 - 0.229 * pitchRatio)));

                                if ((AreaRatio > 0.3 && AreaRatio < 1.05) || AreaRatio == 0.3 || AreaRatio == 1.05)// 0.3 >= AreaRatio >= 1.05
                                {
                                    AreaRatio = (double)(decimal.Round((decimal)AreaRatio, 2)); // Cutoff overplus decimal part of AreaRatio

                                    do
                                        AreaRatio += 0.01;
                                    while (!int.TryParse((AreaRatio / 0.05).ToString(), out INT_tryout));

                                    for (double RealAreaRatio = AreaRatio; RealAreaRatio <= 1.05; RealAreaRatio += 0.05) // i : RoundUp AreaRatio to the availible AreaRatios in B-series
                                    {

                                        Kt = Kt_calculator(J, pitchRatio, RealAreaRatio, Z);

                                        //check for Thrust Range (Min: +1% :: Max:+3%)
                                        if ((Kt * density * Math.Pow((n / 60), 2) * Math.Pow(D, 4)) >= Thrust * 1.01 && (Kt * density * Math.Pow((n / 60), 2) * Math.Pow(D, 4)) <= Thrust * 1.03)
                                        {
                                            Kq = Kq_calculator(J, pitchRatio, RealAreaRatio, Z);

                                            //check for a good efficiency (Min: +50% :: Max:+100%)
                                            if (((J / (2 * Math.PI)) * (Kt / Kq)) >= 0.5 && ((J / (2 * Math.PI)) * (Kt / Kq)) <= 1)
                                            {
                                                //Add propeller to List
                                                Propeller Propeller = new Propeller();
                                                Propeller.AreaRatio = (double)(decimal.Round((decimal)RealAreaRatio, 2)); ;
                                                Propeller.PitchRatio = (double)(decimal.Round((decimal)pitchRatio, 2)); ;
                                                Propeller.BladeNumber = (double)(decimal.Round((decimal)Z, 2)); ;
                                                Propeller.Diameter = (double)(decimal.Round((decimal)D, 2)); ;
                                                Propeller.RPM = (double)(decimal.Round((decimal)n, 2)); ;
                                                Propeller.KT = Kt;
                                                Propeller.KQ = Kq;
                                                Propeller.Thrust = (double)(decimal.Round((decimal)((Kt * density * Math.Pow((n / 60), 2) * Math.Pow(D, 4))), 2));
                                                Propeller.Tourqe = (double)(decimal.Round((decimal)((Kq * density * Math.Pow((n / 60), 2) * Math.Pow(D, 5))), 2));
                                                Propeller.Efficiency = (double)(decimal.Round((decimal)((J / (2 * Math.PI)) * (Kt / Kq)), 4)) * 100;
                                                PropellerList.Add(Propeller);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        void RefreshPropellerDataGrid()
        {
            PropellerDataGrid.Rows.Clear();
            foreach (Propeller propeller in PropellerList)
            {
                PropellerDataGrid.Rows.Add
                    (
                    propeller.Thrust.ToString(),
                    propeller.Tourqe.ToString(),
                    propeller.Efficiency.ToString(),
                    propeller.PitchRatio.ToString(),
                    propeller.AreaRatio.ToString(),
                    propeller.Diameter.ToString(),
                    propeller.BladeNumber.ToString(),
                    propeller.RPM.ToString()
                    );
            }
            if (UserRequest)
            {
                Thrust = 0;
                T = 0;
                AdvanceSpeed = 0;
            }
        }

        private void RUN_Bottom_Click(object sender, EventArgs e)
        {
            //
            //check input
            //
            foreach (Control control in ShipGeometryInput.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    double tryout;
                    if (!double.TryParse(control.Text, out tryout))
                    {
                        MessageBox.Show("Please check the input \nall must be number");
                        return;
                    }
                }
            }
            foreach (Control control in AppendageInput.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    double tryout;
                    if (!double.TryParse(control.Text, out tryout))
                    {
                        MessageBox.Show("Please check the input \nall must be number");
                        return;
                    }
                }
            }

            //
            //set input
            //

            //Geometry
            Lwl = double.Parse(length_wl.Text);
            Lpp = double.Parse(length_bp.Text);
            B = double.Parse(breadth.Text);
            T = double.Parse(draught.Text);
            Displacement = double.Parse(displacement_volume.Text);
            Lcb = double.Parse(long_b_centr.Text);
            Abt = double.Parse(bulbcross_area.Text);
            Hb = double.Parse(Bulb_H.Text);
            Cm = double.Parse(mid_coef.Text);
            Cwp = double.Parse(wp_coef.Text);
            At = double.Parse(transom_area.Text);
            Cstern = double.Parse(stern_coef.Text);
            ServiceSpeed = double.Parse(speed.Text);
            

            // appendage wetted surface area
            rudderbehindskeg = double.Parse(rbskeg.Text);
            rudderbehindester = double.Parse(rbstern.Text);
            twinscrewbalnce = double.Parse(tsbr.Text);
            shaftbracket = double.Parse(sbr.Text);
            skeg = double.Parse(sk.Text);
            strutbossing = double.Parse(stb.Text);
            hullbossing = double.Parse(hullb.Text);
            shaft = double.Parse(sh.Text);
            stablizerfin = double.Parse(stf.Text);
            dome = double.Parse(sonar.Text);
            bilgekeel = double.Parse(bk.Text);

            //calculate resistance
            HoltropPrimaryResults(ServiceSpeed);
            HoltropResistanceDiagrams(ServiceSpeed);

            TabControl.SelectTab(2);
        }

        private void Next_Bottom_Click(object sender, EventArgs e)
        {
            TabControl.SelectTab(1);
        }

        private void SaveExcel_Click(object sender, EventArgs e)
        {
            SaveAsExcel();
        }

        //Search B_W for Propellers
        private void PropellerSearchButton_Click(object sender, EventArgs e)
        {
            foreach (Control control in PropellerInputPanel.Controls)
            {
                if (control is System.Windows.Forms.TextBox)
                {
                    double tryout;
                    if (!double.TryParse(control.Text, out tryout))
                    {
                        MessageBox.Show("Please check the input \nall must be number");
                        return;
                    }
                }
            }

            double D_max = double.Parse(MaximumDiameter_Textbox.Text);
            double D_min = double.Parse(MinimumDiameter_TextBox.Text);
            double D_step = double.Parse(DiameterStepCounter_TextBox.Text);
            double RPM_max = double.Parse(MaximumRPM_Textbox.Text);
            double RPM_min = double.Parse(MinimumRPM_Textbox.Text);
            double RPM_step = double.Parse(RpmStepConter_Textbox.Text);

            //double D_max = 0.1;
            //double D_min = 0.04;
            //double D_step = 0.001;
            //double RPM_max = 20000;
            //double RPM_min = 10;
            //double RPM_step = 10;
            //double ServiceSpeed = 1.4;
            //int[] PropellerBladesNumbers = { 2, 3, 4, 5, 6, 7 };

            Search_B_W(ServiceSpeed, D_max, D_min, D_step, RPM_max, RPM_min, RPM_step, PropellerBladesNumbers);

            RefreshPropellerDataGrid();
        }

        private void PropellerRefreshButton_Click(object sender, EventArgs e)
        {
            ///
            //PlaceHolder TEXT for Propeller parameters(Rpm,Diameter) range 
            ///

            //Diameter::GotFocus
            MaximumDiameter_Textbox.GotFocus += MaximumDiameter_Textbox_GotFocus;
            MinimumDiameter_TextBox.GotFocus += MinimumDiameter_TextBox_GotFocus;
            DiameterStepCounter_TextBox.GotFocus += DiameterStepCounter_TextBox_GotFocus;
            //Diameter::LostFocus
            MaximumDiameter_Textbox.LostFocus += MaximumDiameter_Textbox_LostFocus;
            MinimumDiameter_TextBox.LostFocus += MinimumDiameter_TextBox_LostFocus;
            DiameterStepCounter_TextBox.LostFocus += DiameterStepCounter_TextBox_LostFocus;

            //RPM::GotFocus
            MaximumRPM_Textbox.GotFocus += MaximumRPM_Textbox_GotFocus;
            MinimumRPM_Textbox.GotFocus += MinimumRPM_Textbox_GotFocus;
            RpmStepConter_Textbox.GotFocus += RpmStepConter_Textbox_GotFocus;
            //RPM::LostFocus
            MaximumRPM_Textbox.LostFocus += MaximumRPM_Textbox_LostFocus;
            MinimumRPM_Textbox.LostFocus += MinimumRPM_Textbox_LostFocus;
            RpmStepConter_Textbox.LostFocus += RpmStepConter_Textbox_LostFocus;

            //Initial text 
            DiameterStepCounter_TextBox.Text = "0.1";
            MaximumDiameter_Textbox.Text = (T).ToString();
            MinimumDiameter_TextBox.Text = (T/2).ToString();

            RpmStepConter_Textbox.Text = "5";
            MaximumRPM_Textbox.Text = "1000";
            MinimumRPM_Textbox.Text = "50";
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex == 4)
            {
                PropellerRefreshButton_Click(new object(), new EventArgs());
            }
        }

        private void Blades_CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(Blades_CheckBox2.Checked)
            {
                PropellerBladesNumbers[0] = 2;
            }
            if(!Blades_CheckBox2.Checked)
            {
                PropellerBladesNumbers[0] = 0;
            }
        }

        private void Blades_CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (Blades_CheckBox3.Checked)
            {
                PropellerBladesNumbers[1] = 3;
            }
            if (!Blades_CheckBox3.Checked)
            {
                PropellerBladesNumbers[1] = 0;
            }
        }

        private void Blades_CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (Blades_CheckBox4.Checked)
            {
                PropellerBladesNumbers[2] = 4;
            }
            if (!Blades_CheckBox4.Checked)
            {
                PropellerBladesNumbers[2] = 0;
            }
        }

        private void Blades_CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (Blades_CheckBox5.Checked)
            {
                PropellerBladesNumbers[3] = 5;
            }
            if (!Blades_CheckBox5.Checked)
            {
                PropellerBladesNumbers[3] = 0;
            }
        }

        private void Blades_CheckBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (Blades_CheckBox6.Checked)
            {
                PropellerBladesNumbers[4] = 6;
            }
            if (!Blades_CheckBox6.Checked)
            {
                PropellerBladesNumbers[4] = 0;
            }
        }

        private void Blades_CheckBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (Blades_CheckBox7.Checked)
            {
                PropellerBladesNumbers[5] = 7;
            }
            if (!Blades_CheckBox7.Checked)
            {
                PropellerBladesNumbers[5] = 0;
            }
        }
        #endregion
    }
}
