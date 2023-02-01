using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace PC2023_Part2
{
    public class DeconstructBeam : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructBeam class.
        /// </summary>
        public DeconstructBeam()
          : base("DeconstructBeam", "decBeam",
              "Description",
              "NTNU", "PC2023_Part2")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("beam","b","beamClass object", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("name","n","",GH_ParamAccess.item);
            pManager.AddIntegerParameter("id", "i", "", GH_ParamAccess.item);
            pManager.AddLineParameter("axis", "a", "", GH_ParamAccess.item);
            pManager.AddBrepParameter("brep", "b", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            BeamClass bc = new BeamClass();
            DA.GetData(0, ref bc);

            string n = "name";
            if (bc.name != null)
                n = bc.name;

            int i = 0;
            if (bc.id != null)
                i = bc.id;

            Line l = new Line();
            if (bc.axis != null)
                l = bc.axis;

            Brep b = new Brep();
            if (bc.brep != null)
                b = bc.brep;

            DA.SetData(0,n);
            DA.SetData(1, i);
            DA.SetData(2, l);
            DA.SetData(3, b);

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
            get { return new Guid("DC45B7DA-35DB-463F-9842-403FBC2B865C"); }
        }
    }
}