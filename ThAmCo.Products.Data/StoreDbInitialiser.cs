using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThAmCo.Products.Data
{
    public static class StoreDbInitialiser
    {
        public static async Task SeedTestData(StoreDb context, IServiceProvider services)
        {
            if (context.Products.Any())
            {
                //db seems to be seeded
                return;
            }

            var types = new List<PType>
            {
                new PType { Name = "Chronograph", Description = "Looking for a chronograph watch? We’ve collated all the best styles so you can find a watch that suits your unique tastes. Choose from a range of stylish designs, from chunky metal link bracelets to bright coloured rubber straps and traditional leather watches.", Active = true },
                new PType { Name = "Digital", Description = "Digital watches are an ideal choice if you want a highly functional timepiece that can be used for sporty activities as well as for everyday use. Many of the styles boast special features such as easy-to-read displays, alarm functionality, stopwatch, variable time zone formats, backlights, and much more.", Active = true },
                new PType { Name = "Slim Case", Description = "Keep it simple with a slim case watch. Our collection features a range ultra-slim designs with a sophisticated finish. Lightweight in design, a slim case watch is an ideal choice if you want a watch that offers supreme comfort.", Active = true },
                new PType { Name = "Smart", Description = "Up your fitness game, connect to your favourite apps, and keep on top of social media – all from your wrist. A smartwatch allows you to stay in touch with the world by connecting to your smartphone via cutting-edge technology, with features including GPS, bluetooth connectivity, fitness trackers and more.", Active = true },
                new PType { Name = "Automatic", Description = "Like to keep your style traditional? Our self-winding automatic watches are an ideal choice for those who want a watch that’s timeless, functional, and guaranteed to stand the test of time. Explore a variety of signature styles for men and women, from chunky stainless-steel models to durable leather designs.", Active = true },
                new PType { Name = "Sports", Description = "Sport watches are an ideal choice for those who enjoy outdoor activities or want some assistance in achieving their fitness goals. Our collection features watches loaded with features including stopwatches, tachymeters, compasses, and thermometers.", Active = true },
                new PType { Name = "Waterproof", Description = "Waterproof watches are a great choice if you enjoy water sports, swimming, sailing, or if you simply want a watch that can survive extremely wet conditions. Watches from the collection can withstand depths of between 30-1000 metres and can come packed with high-performance features such as world time function, speed display, and shock resistance.", Active = true },
                new PType { Name = "Childrens", Description = "Whether you're teaching your child to tell the time or need something trendy and robust that can survive playtime, our extensive collection of kids' watches features the funkiest brands, such as Sekonda, Ice-Watch, or Limit, and the most popular cartoon franchises, making them an ideal gift for children of all ages.", Active = true }
            };
            types.ForEach(t => context.Types.Add(t));

            var materials = new List<Material>
            {
                new Material { Name = "Titanium", Active = true },
                new Material { Name = "Rose Gold", Active = true },
                new Material { Name = "Gold", Active = true },
                new Material { Name = "Silver", Active = true },
                new Material { Name = "Ceramic", Active = true },
                new Material { Name = "Plastic ", Active = true }
            };
            materials.ForEach(m => context.Materials.Add(m));

            var brands = new List<Brand>
            {
                new Brand { Name = "Armani Exchange", Description = "AX Armani Exchange is the youthful label created in 1991 by iconic Italian designer Giorgio Armani, offering men's and women's clothing and accessories that are inspired by the designer's codes of style. AX Armani Exchange captures the heritage of the Armani brand through a modern sensibility.", Active = true },
                new Brand { Name = "Bulova", Description = "Since 1875 Bulova has been at the forefront of watch technology, innovation and industry firsts. Having opened its first store in Manhattan, Bulova has become internationally recognised for high quality materials, high accuracy and unique designs. This year Bulova heritages its past through its Archive series and re-introduces Computron, one of the world’s first LED quartz watches from the 1970’s. Equally stunning is Precisionist, with a 262 khz High Performance Quartz movement, which is 8 times more accurate than standard quartz, a 1/1000th of a second chronograph and elegant sweep motion second hand all topped off with 300m water resistance. One of Bulova’s headline collections is Curv, with the world’s first curved chronograph movement, bringing 262khz accuracy, slimline elegance this year shown in vivid blue within a PVD plated black case. Stunning and Unique.", Active = true },
                new Brand { Name = "Calvin Klein", Description = "CALVIN KLEIN WATCHES + JEWELLERY is a global lifestyle brand that embodies progressive ideals and a seductive and minimal aesthetic. We seek to thrill and inspire our forward-thinking audience while using provocative imagery, and a contemporary and instantly recognizable design. Founded in 1997 when CALVIN KLEIN and the Swatch Group merged their formidable and unique talents to create cK Watch and again later in 2004 with the launch of the jewellery collection, CALVIN KLEIN WATCHES + JEWELLERY is today one of the leading and most influential global fashion brands.", Active = true },
                new Brand { Name = "Casio", Description = "Casio watches hinge on the philosophy of 'creativity and contribution' – innovation pulses through each of the brand's designs. After the launch of the first Casio timepiece in the 1970s, the Japanese company became a leader in the digital watch world.The Casio empire has only grown . Today, the brand creates pieces that defy gravity, challenge the elements, and push the boundaries of both style and technology. From ultra-sporty, feature-packed designs to retro-inspired fashion timepieces,the Casio range has something for everyone.", Active = true },
                new Brand { Name = "Certina", Description = "Certina sport watches have all the same qualities that make a great athlete - endurance, strength, precision, performance and reliability. As a serious sports watch brand, Certina crafts timepieces that meet the highest demands of quality and design, with precision engineering and comprehensive protective features built in.", Active = true },
                new Brand { Name = "Gucci", Description = "In 1972, Gucci became one of the first fashion Houses to branch into timepieces, creating successful, iconic models that combined contemporary spirit and tradition, innovation and craftsmanship, fashion and elegance. Since that time, Gucci timepieces have been made in Switzerland, assembled at the company’s watchmaking atelier in La Chaux-de-Fonds. It is this marriage of Swiss manufacturing traditions using high quality components together with Gucci detailing and Italian flair that has enabled the brand to enjoy over 40 years of watchmaking history. Today, Gucci watches are synonymous with fine quality as they bring a fresh, innovative perspective to the watch industry.", Active = true },
                new Brand { Name = "Guess", Description = "Based in Los Angeles, California, GUESS is one of the most widely recognized brands in the world. As it progresses into 21st century, GUESS continues to challenge its already high standards to remain a driving artistic force in the world of fashion.", Active = true },
                new Brand { Name = "Hugo", Description = "HUGO watches speak the language of self-expression fluently. They are about individual accents that reflect wearers’ uniqueness, losing nothing in translation. Opportunities replace rules in designs with attitude for wearers to match. Originality is the common denominator of a broad spectrum of eye-catching designs with some teasing twists. These timepieces are the ideal accomplices for up-to-the-minute lifestyles where comfort zones rarely appear on the map. Personality is not negotiable.", Active = true },
                new Brand { Name = "Lacoste", Description = "Sporty, sophisticated, and effortlessly cool, Lacoste watches represent everything the legendary athletics brand is adored for. Created by French tennis legend René Lacoste, who was nicknamed ‘the crocodile' due to his on-court tenacity, the first Lacoste polo shirt debuted in 1933. It featured the emblematic croc motif and became the go-to uniform for the more discerning player. 60 years later, Lacoste released its first watch, which replicated the fresh, preppy look of the classic polo shirt. Today, the collection is even more diverse. From sophisticated, handsome pieces that replace the croc with a more subtle Lacoste logo to colour-popping models, there's a Lacoste watch to suit everyone's sporty, sophisticated, and stylish side.", Active = true },
                new Brand { Name = "Emporio Armani ", Description = "The Emporio Armani collection is infused with an understated sense of confidence, offering up classic watch styles imbued with heritage and enduring design. Emporio Armani is \"a line for men and women who lead a modern lifestyle and want to dress with a sense of casual sophistication.Emporio Armani watches reflect this approach with modern shapes and materials, balanced with a classic style, \" Giorgio Armani.", Active = true }
            };
            brands.ForEach(b => context.Brands.Add(b));

            await context.SaveChangesAsync();

            Random random = new Random();
            var products = new List<Product>
            {
                new Product { Type = types[0], Material = materials[0], Brand = brands[0], Name = "Watch 1", Description = "This is a description for watch 1.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[1], Material = materials[1], Brand = brands[1], Name = "Watch 2", Description = "This is a description for watch 2.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[2], Material = materials[2], Brand = brands[2], Name = "Watch 3", Description = "This is a description for watch 3.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[3], Material = materials[3], Brand = brands[3], Name = "Watch 4", Description = "This is a description for watch 4.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[4], Material = materials[4], Brand = brands[4], Name = "Watch 5", Description = "This is a description for watch 5.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[5], Material = materials[5], Brand = brands[5], Name = "Watch 6", Description = "This is a description for watch 6.", Price = Math.Round(100 + (random.NextDouble() * (1000 - 100)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[6], Material = materials[0], Brand = brands[6], Name = "Watch 7", Description = "This is a description for watch 7.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[7], Material = materials[1], Brand = brands[7], Name = "Watch 8", Description = "This is a description for watch 8.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[0], Material = materials[2], Brand = brands[8], Name = "Watch 9", Description = "This is a description for watch 9.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[1], Material = materials[3], Brand = brands[9], Name = "Watch 10", Description = "This is a description for watch 10.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[2], Material = materials[4], Brand = brands[0], Name = "Watch 11", Description = "This is a description for watch 11.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[3], Material = materials[5], Brand = brands[1], Name = "Watch 12", Description = "This is a description for watch 12.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[4], Material = materials[0], Brand = brands[2], Name = "Watch 13", Description = "This is a description for watch 13.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[5], Material = materials[1], Brand = brands[3], Name = "Watch 14", Description = "This is a description for watch 14.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[6], Material = materials[2], Brand = brands[4], Name = "Watch 15", Description = "This is a description for watch 15.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[7], Material = materials[3], Brand = brands[5], Name = "Watch 16", Description = "This is a description for watch 16.", Price = Math.Round(100 + (random.NextDouble() * (1000 - 100)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[0], Material = materials[4], Brand = brands[6], Name = "Watch 17", Description = "This is a description for watch 17.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[1], Material = materials[5], Brand = brands[7], Name = "Watch 18", Description = "This is a description for watch 18.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[2], Material = materials[0], Brand = brands[8], Name = "Watch 19", Description = "This is a description for watch 19.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = random.Next(0, 20), Active = true },
                new Product { Type = types[3], Material = materials[1], Brand = brands[9], Name = "Watch 20", Description = "This is a description for watch 20.", Price = Math.Round(10 + (random.NextDouble() * (100 - 10)), 2), StockLevel = 0, Active = true },
            };
            products.ForEach(p => context.Products.Add(p));

            await context.SaveChangesAsync();
        }
    }
}
