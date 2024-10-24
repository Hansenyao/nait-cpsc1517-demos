using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPLibrary
{
    public record ExampleRecord
    {
        //Can also define properties
        //init means to initialize the property once.
        //Record are primarily immutable (they cannot change)
        public string exampleString { get; init; }

        //you can create a record with a mutable (changable) property, but there are very few use cases (uses) for this
        //If you are going to have properties that can be set, you should likely make this a class.
        public string exampleMutation { get; set; }
    }
}