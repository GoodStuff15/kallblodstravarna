using resortlibrary.Models;

namespace resortlibrary.Builders
{
    public class AdditionalOptionBuilder
    {
        private AdditionalOption _additionalOption;

        public AdditionalOptionBuilder()
        {
            _additionalOption = new AdditionalOption();
        }

        public AdditionalOptionBuilder AddName(string name)//Set name of additional option. Throw if null och whitespace
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required.");
            }

            _additionalOption.Name = name;
            return this;
        }

        public AdditionalOptionBuilder AddDescription(string description)//Set description
        {
            _additionalOption.Description = description;
            return this;
        }

        public AdditionalOptionBuilder AddPrice(decimal price)//Set price. Must be greater than 0
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be greater than 0.");
            }

            _additionalOption.Price = price;
            return this;
        }

        public AdditionalOptionBuilder AddPerGuest(bool perGuest)//Set option-charge per guest.
        {
            _additionalOption.PerGuest = perGuest;
            return this;
        }

        public AdditionalOptionBuilder AddPerNight(bool perNight)//Set option-charge per night.
        {
            _additionalOption.PerNight = perNight;
            return this;
        }

        public AdditionalOption Build()//Build additional options-object
        {
            return _additionalOption;
        }
    }
}
