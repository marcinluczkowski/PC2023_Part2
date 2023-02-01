using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace PC2023_Part2
{
    internal class BeamClass
    {
        //properties
        public Line axis;
        public Brep brep;
        public string name;
        public int id;

        public List<Point3d> intersectionPoints;
        public List<DetailClass> details;


        //constructors
        public BeamClass()
        { 
            //empty
        }

        public BeamClass(string _name, int _id, Line _axis)
        { 
            name = _name;
            id = _id;
            axis = _axis;
        }


    }
}
