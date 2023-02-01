using Grasshopper;
using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace PC2023_Part2
{
    public class PC2023_Part2Info : GH_AssemblyInfo
    {
        public override string Name => "PC2023_Part2";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => null;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "";

        public override Guid Id => new Guid("CCA850BD-D8B0-418A-8EA7-82840A9A012E");

        //Return a string identifying you or your company.
        public override string AuthorName => "";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "";
    }
}