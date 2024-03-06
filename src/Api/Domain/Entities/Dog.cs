namespace DogShelterService.Api.Domain.Entities
{
    public class Dog
    {
        public string Name { get; private set; }
        public int AverageHeight { get; private set; }
        public Breed Breed { get; private set; }

        private Dog(string name, Breed breed)
        {
            this.Name = name;
            this.Breed = breed;
        }

        public static Dog Create(string name, Breed breed)
        {
            Dog dog = new Dog(name, breed);
            dog.AverageHeight = breed.GetAverageHeight();
            return dog;
        }
    }
}
