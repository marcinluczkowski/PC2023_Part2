﻿using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC2023_Part2
{
    internal class DetailClass
    {
        //properties
        public Brep brep;
        public string name;
        public int id;

        public Point3d location;

        //constructor 
        public DetailClass()
        { }

        public DetailClass(string _name, int _id , Point3d _location)
        { 
         name = _name;
            id = _id;
            location = _location;
        }
    }
}
