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

            double height = 100;
            double thickness = 10;
            for (int i = 0; i < bcs.Count; i++)
            {
                BeamClass bc = bcs[i];

                Line axis = bc.axis;

                if (bc.name == "horizontalBeam")
                {
                    var t1 = Transform.Translation(new Vector3d(0, 1, 0));
                    Line line1 = new Line(axis.From, axis.To);
                    line1.Transform(t1);
                    bc.brep = Brep.CreateFromCornerPoints(axis.To, axis.From, line1.From, line1.To, 0.00001);
                }
                else if (bc.name == "verticalBeam")
                {
                    var t2 = Transform.Translation(new Vector3d(1, 0, 0));
                    Line line2 = new Line(axis.From, axis.To);
                    line2.Transform(t2);
                    bc.brep = Brep.CreateFromCornerPoints(axis.To, axis.From, line2.From, line2.To, 0.00001);
                }
            }

            DA.SetDataList(0, bcs);
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