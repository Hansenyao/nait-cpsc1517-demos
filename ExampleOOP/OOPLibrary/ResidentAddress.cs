namespace OOPLibrary
{
    public record ResidentAddress(int Number, string Street, string City, string Region, string PostalCode)
    {
        //this is a record - a record behaves like a class
        //read-only version of a class once the instance is created.
        //Once created and provided the parameters, a record cannot be changed/altered

        //If you ever need to alter a record, you need to create a new record

        //This method of defining a record acts as the greedy Constructor
        //takes in the parameter list, no other option
        //All properties in this case are immutable (cannot be changed)

        //We can add a override for the ToString method for a record :)
        public override string ToString() => $"{Number},{Street},{City},{Region},{PostalCode}";
    }
}