using System;
using System.Collections.Generic;
using System.Security.Policy;
using Eto.Forms;
using Grasshopper.Kernel;
using Rhino.Geometry;

namespace PC2023_Part2
{
    public class MakeBrep : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the MakeBrep class.
        /// </summary>
        public MakeBrep()
          : base("MakeBrep", "Nickname",
              "Description",
              "NTNU", "PC2023_Part2")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("beams","bs","list of beamClass objects",GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("beams", "bs", "list of beamClass objects", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<BeamClass> bcs = new List<BeamClass>();
            DA.GetDataList(0, bcs);

            List<BeamClass> nbcs = new List<BeamClass>(); //declare a new list of Beam class objects
            double height = 100;
            double thickness = 10;
            for (int i = 0; i < bcs.Count; i++)
            {
                BeamClass bc = new BeamClass(bcs[i].name, bcs[i].id, bcs[i].axis); //create new instance of the class
                Line axis = bc.axis;  //take the line of the beamClass object

                if (bc.name == "horizontalBeam")
                {
                    var t11 = Transform.Translation(new Vector3d(0, -thickness/2 , 0));
                    var t12 = Transform.Translation(new Vector3d(0, thickness / 2, 0));
                    Line line11 = new Line(axis.From, axis.To);
                    Line line12 = new Line(axis.From, axis.To);
                    line11.Transform(t11);
                    line12.Transform(t12);
                    var pl = new Polyline(
                        new List<Point3d>()
                        {line11.From, line11.To, line12.To, line12.From, line11.From  }
                        );

                    Curve section = pl.ToNurbsCurve();
                    Line rail = new Line(line11.From, new Point3d(line11.FromX, line11.FromY, line11.FromZ + height));
                    var brps = Brep.CreateFromSweep(rail.ToNurbsCurve(), section, true, 0.00001);
                    bc.brep = brps[0];//Brep.CreateFromCornerPoints(line11.To, line11.From, line12.From, line12.To, 0.00001); //adding brep
                }
                else if (bc.name == "verticalBeam")
                {
                    var t21 = Transform.Translation(new Vector3d(-thickness / 2, 0, 0));
                    var t22 = Transform.Translation(new Vector3d(thickness / 2, 0, 0));
                    var line21 = new Line(axis.From, axis.To);
                    var line22 = new Line(axis.From, axis.To);
                    line21.Transform(t21);
                    line22.Transform(t22);
                    var pl = new Polyline(
                        new List<Point3d>()
                        {line21.From, line21.To, line22.To, line22.From, line21.From  }
                        );

                    Curve section = pl.ToNurbsCurve();
                    Line rail = new Line(line21.From, new Point3d(line21.FromX, line21.FromY, line21.FromZ + height));
                    var brps = Brep.CreateFromSweep(rail.ToNurbsCurve(), section, true, 0.00001);
                    bc.brep = brps[0];
                }
                nbcs.Add(bc);  //adding new instance to the list
            }

            DA.SetDataList(0, nbcs);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("95AFF64C-931F-4BEE-9B3B-0D4658D18944"); }
        }
    }
}