using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using Rhino.Geometry.Intersect;

namespace PC2023_Part2
{
    public class PC2023_Part2Component : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public PC2023_Part2Component()
          : base("SortCurves", "scrvs",
            "sorting curves on horizontal and vertical",
            "NTNU", "PC2023_Part2")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddCurveParameter("curves", "cs", "curves to sort", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddCurveParameter("vcurves", "vcs", "vertical curves", GH_ParamAccess.list); //0
            pManager.AddCurveParameter("hcurves", "hcs", "horizontal curves", GH_ParamAccess.list); //1
            pManager.AddPointParameter("ipoints", "ipts","intersection points", GH_ParamAccess.list) ; //2
            pManager.AddGenericParameter("beams","bcs","beamClass objects",GH_ParamAccess.list); //3
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Curve> crvs = new List<Curve>();
            DA.GetDataList(0, crvs);

            var hcrvs = new List<Curve>();
            var vcrvs = new List<Curve>();
            //use our method
            getVerticalHorizontalCurves(crvs, out hcrvs, out vcrvs);

            List<Point3d> ipts = getIntBetweenHandV(vcrvs, hcrvs);

            List<BeamClass> bcs = new List<BeamClass>();
            int idv = 0;
            foreach (var v in vcrvs)
            {
                BeamClass bc = new BeamClass("verticalBeam",idv, new Line(v.PointAtStart, v.PointAtEnd));
                idv++;
                bcs.Add(bc);
            }
            int idh = 0;
            foreach (var h in hcrvs)
            {
                BeamClass bc = new BeamClass("horizontalBeam", idh, new Line(h.PointAtStart, h.PointAtEnd));
                idh++;
                bcs.Add(bc);
            }

            DA.SetDataList(0, vcrvs);
            DA.SetDataList(1, hcrvs);
            DA.SetDataList(2, ipts);
            DA.SetDataList(3, bcs);
        }

        //methods
        List<Point3d> getIntBetweenHandV(List<Curve> vcrvs, List<Curve> hcrvs)
        {
            List<Point3d> ipts = new List<Point3d>();
            foreach (var vc in vcrvs)
            {
                foreach (var hc in hcrvs)
                {
                    var inter = Intersection.CurveCurve(vc, hc, 0.0001, 0.0001);
                    foreach (var i in inter)
                    {
                        ipts.Add(i.PointA);
                    }
                }
            }
            return ipts;
        }

        void getVerticalHorizontalCurves(List<Curve> icrvs, out List<Curve> hcrvs, out List<Curve> vcrvs)
        {
            List<Curve> hcrvs1 = new List<Curve>();
            List<Curve> vcrvs1 = new List<Curve>();
            //place for the code
            foreach (Curve c in icrvs)
            {
                Vector3d v = c.TangentAtStart;
                v.Unitize();
                if (Math.Abs(v.X) == 1)
                {
                    hcrvs1.Add(c);
                }
                else if (Math.Abs(v.Y) == 1)
                {
                    vcrvs1.Add(c);
                }
                else
                { 
                
                }
            }

            hcrvs = hcrvs1;
            vcrvs = vcrvs1;
        }


        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("A5D6675A-96B2-416D-8ACE-E274F4002055");
    }
}