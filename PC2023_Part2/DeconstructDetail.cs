using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace PC2023_Part2
{
    public class DeconstructDetail : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the DeconstructDetail class.
        /// </summary>
        public DeconstructDetail()
          : base("DeconstructDetail", "Nickname",
              "Description",
              "NTNU", "PC2023_Part2")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("detail", "d","",GH_ParamAccess.item) ;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("name","n","",GH_ParamAccess.item);
            pManager.AddIntegerParameter("type", "t", "", GH_ParamAccess.item);
            pManager.AddPointParameter("point", "pt", "", GH_ParamAccess.item);
            pManager.AddBrepParameter("brep", "br", "", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            DetailClass dc = new DetailClass();
            DA.GetData(0, ref dc);

            string n = "name";
            if (dc.name != null)
                n = dc.name;

            int i = 0;
            if (dc.id != null)
                i = dc.id;

            Point3d p = new Point3d();
            if (dc.location != null)
                p = dc.location;

            Brep b = new Brep();
            if (dc.brep != null)
                b = dc.brep;

            DA.SetData(0, n);
            DA.SetData(1, i);
            DA.SetData(2, p);
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
            get { return new Guid("E63990AC-E15D-4468-A0A2-1CA61396E5EE"); }
        }
    }
}