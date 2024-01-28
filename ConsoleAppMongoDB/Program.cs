using System;
using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;


class Program
{
    static void Main()
    {
        try
        {
            // Connect to MongoDB
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Coop_Taxi");

            Console.WriteLine("Connected to MongoDB");

            // Create a collection
            var collectionSocio = database.GetCollection<BsonDocument>("Socio");
            var collectionUnidad = database.GetCollection<BsonDocument>("Unidad");


            // Main loop
            while (true)
            {
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Crear un Socio");
                Console.WriteLine("2. Crear una Unidad");

                Console.WriteLine("3. Ver los Socios");
                Console.WriteLine("4. Ver las Unidades");

                Console.WriteLine("5. Actualizar info de un Socio");
                Console.WriteLine("6. Actualizar info de una Unidad");

                Console.WriteLine("7. Eliminar un Socio");
                Console.WriteLine("8. Eliminar una Unidad");

                Console.WriteLine("0. Exit");

                Console.Write("Selecciona una acción: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateSocio(collectionSocio);
                        break;

                    case "2":
                        ReadSocio(collectionSocio);
                        CreateUnidad(collectionUnidad);
                        break;

                    case "3":
                        ReadSocio(collectionSocio);
                        break;

                    case "4":
                        ReadUnidad(collectionUnidad);
                        break;


                    case "5":
                        UpdateSocio(collectionSocio);
                        break;

                    case "6":
                        UpdateUnidad(collectionUnidad);
                        break;

                    case "7":
                        DeleteSocio(collectionSocio);
                        break;

                    case "8":
                        DeleteUnidad(collectionUnidad);
                        break;

                    case "0":
                        Console.WriteLine("Exiting the application.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void CreateSocio(IMongoCollection<BsonDocument> collection)
    {
        // Prompt the user for input
        Console.WriteLine("Ingrese el nombre:");
        string nombre = Console.ReadLine();

        Console.WriteLine("Ingrese el apellido:");
        string apellido = Console.ReadLine();

        Console.WriteLine("Ingrese la cédula:");
        string cedula = Console.ReadLine();

        Console.WriteLine("Ingrese el tipo de licencia:");
        string tipoLicencia = Console.ReadLine();

        Console.WriteLine("Ingrese el sector de la dirección:");
        string sector = Console.ReadLine();

        Console.WriteLine("Ingrese la calle principal de la dirección:");
        string callePrincipal = Console.ReadLine();

        Console.WriteLine("Ingrese la calle secundaria de la dirección:");
        string calleSecundaria = Console.ReadLine();

        Console.WriteLine("Ingrese el número de la dirección:");
        string numero = Console.ReadLine();

        // Create the BsonDocument using the user input
        var document = new BsonDocument
    {
        { "Nombre", nombre },
        { "Apellido", apellido },
        { "Cedula", cedula },
        { "Tipo_de_Licencia", tipoLicencia },
        { "Direccion", new BsonDocument
            {
                { "Sector", sector },
                { "Calle_principal", callePrincipal },
                { "Calle_secundaria", calleSecundaria },
                { "Numero", numero }
            }
        }
    };

        // Insert the document into the collection
        collection.InsertOne(document);

        Console.WriteLine("Socio Creado Exitosamente.");
    }


    static void CreateUnidad(IMongoCollection<BsonDocument> collection)
    {
        // Prompt the user for input
        Console.WriteLine("Ingrese el ID del Socio:");
        string socioIdString = Console.ReadLine();
        ObjectId socioId;
        if (!ObjectId.TryParse(socioIdString, out socioId))
        {
            Console.WriteLine("ID del Socio no válido.");
            return;
        }

        Console.WriteLine("Ingrese el número:");
        string numero = Console.ReadLine();

        Console.WriteLine("Ingrese el chasis:");
        string chasis = Console.ReadLine();

        Console.WriteLine("Ingrese el motor:");
        string motor = Console.ReadLine();

        Console.WriteLine("Ingrese la marca de las características:");
        string marca = Console.ReadLine();

        Console.WriteLine("Ingrese el modelo de las características:");
        string modelo = Console.ReadLine();

        Console.WriteLine("Ingrese el color original de las características:");
        string colorOriginal = Console.ReadLine();

        Console.WriteLine("Ingrese la cilindrada de las características:");
        string cilindrada = Console.ReadLine();

        // Create the BsonDocument using the user input
        var document = new BsonDocument
        {
            { "Socio", socioId },
            { "Numero", numero },
            { "Chasis", chasis },
            { "Motor", motor },
            { "Caracteristicas", new BsonDocument
                {
                    { "Marca", marca },
                    { "Modelo", modelo },
                    { "Color_original", colorOriginal },
                    { "Cilindrada", cilindrada }
                }
            }
        };

        // Insert the document into the collection
        collection.InsertOne(document);

        Console.WriteLine("Unidad Creada Exitosamente.");
    }


    static void ReadSocio(IMongoCollection<BsonDocument> collection)
    {
        // Read all documents in the collection
        var documents = collection.Find(new BsonDocument()).ToList();

        Console.WriteLine("Read documents:");
        Console.WriteLine("\n\n| " + "Id" +
                              "| " + "Nombre" +
                              "| " + "Apellido" +
                              "| " + "Cedula" +
                              "| " + "Tipo_de_Licencia" +
                              "| " + "Sector" +
                              "| " + "Calle_principal" +
                              "| " + "Calle_secundaria" +
                              "| " + "Numero");

        foreach (var document in documents)
        {
            // Deserialize the BsonDocument into a C# object
            var socio = BsonSerializer.Deserialize<Socio>(document);
            Console.WriteLine("| " +socio.Id+ 
                              "| " +socio.Nombre + 
                              "| " +socio.Apellido + 
                              "| " +socio.Cedula +
                              "| " + socio.Tipo_de_Licencia +
                              "| " + socio.Direccion.Sector +
                              "| " + socio.Direccion.Calle_principal +
                              "| " + socio.Direccion.Calle_secundaria +
                              "| " + socio.Direccion.Numero);
        }
    }

    
    static void ReadUnidad(IMongoCollection<BsonDocument> collection)
    {
        // Read all documents in the collection
        var documents = collection.Find(new BsonDocument()).ToList();

        Console.WriteLine("Read documents:");

        Console.WriteLine("\n\n| " + "Id Unidad" +
                             "| " + "Id Socio" +
                             "| " + "Número" +
                             "| " + "Chasis" +
                             "| " + "Motor" +
                             "| " + "Marca" +
                             "| " + "Modelo" +
                             "| " + "Color original" +
                             "| " + "Cilindrada");

        foreach (var document in documents)
        {
            // Deserialize the BsonDocument into a C# object
            var unidad = BsonSerializer.Deserialize<Unidad>(document);
            Console.WriteLine("| " + unidad.Id +
                              "| " + unidad.Socio +
                              "| " + unidad.Numero + 
                              "| " + unidad.Chasis + 
                              "| " + unidad.Motor + 
                              "| " + unidad.Caracteristicas.Marca +
                              "| " + unidad.Caracteristicas.Modelo +
                              "| " + unidad.Caracteristicas.Color_original +
                              "| " + unidad.Caracteristicas.Cilindrada);
        }
    }


    static void UpdateSocio(IMongoCollection<BsonDocument> collection)
    {
        Console.WriteLine("Ingrese el ID del Socio a Editar:");
        string idSocio = Console.ReadLine();

        // Parse the idSocio string into an ObjectId
        ObjectId socioId;
        if (!ObjectId.TryParse(idSocio, out socioId))
        {
            Console.WriteLine("ID del Socio no válido.");
            return;
        }
        // Create a filter to find the document to update
        var filter = Builders<BsonDocument>.Filter.Eq("_id", socioId);

        Console.WriteLine("Ingrese el nombre:");
        string nombre = Console.ReadLine();

        Console.WriteLine("Ingrese el apellido:");
        string apellido = Console.ReadLine();

        Console.WriteLine("Ingrese la cédula:");
        string cedula = Console.ReadLine();

        Console.WriteLine("Ingrese el tipo de licencia:");
        string tipoLicencia = Console.ReadLine();

        Console.WriteLine("Ingrese el sector de la dirección:");
        string sector = Console.ReadLine();

        Console.WriteLine("Ingrese la calle principal de la dirección:");
        string callePrincipal = Console.ReadLine();

        Console.WriteLine("Ingrese la calle secundaria de la dirección:");
        string calleSecundaria = Console.ReadLine();

        Console.WriteLine("Ingrese el número de la dirección:");
        string numero = Console.ReadLine();

        // Create an update definition to set the value of the "Nombre" field to "CAMBIADO"
        var updateDefinition = Builders<BsonDocument>.Update
            .Set("Nombre", nombre)
            .Set("Apellido", apellido)
            .Set("Cedula", cedula)
            .Set("Tipo_de_Licencia", tipoLicencia)
            .Set("Sector", sector)
            .Set("Calle_principal", callePrincipal)
            .Set("Calle_secundaria", calleSecundaria)
            .Set("Numero", numero);

        // Update the document
        var result = collection.UpdateOne(filter, updateDefinition);

        // Check if the update was successful
        if (result.ModifiedCount > 0)
        {
            Console.WriteLine("Documento actualizado correctamente.");
        }
        else
        {
            Console.WriteLine("No se encontró ningún documento para actualizar.");
        }
    }


    static void UpdateUnidad(IMongoCollection<BsonDocument> collection)
    {
        Console.WriteLine("Ingrese el ID de la Unidad a Editar:");
        string idUnidad = Console.ReadLine();

        // Parse the idUnidad string into an ObjectId
        ObjectId unidadId;
        if (!ObjectId.TryParse(idUnidad, out unidadId))
        {
            Console.WriteLine("ID de la Unidad no válido.");
            return;
        }

        // Create a filter to find the document to update
        var filter = Builders<BsonDocument>.Filter.Eq("_id", unidadId);

        // Prompt the user for input
        Console.WriteLine("Ingrese el ID del Socio:");
        string socioIdString = Console.ReadLine();
        ObjectId socioId;
        if (!ObjectId.TryParse(socioIdString, out socioId))
        {
            Console.WriteLine("ID del Socio no válido.");
            return;
        }

        Console.WriteLine("Ingrese el número:");
        string numero = Console.ReadLine();

        Console.WriteLine("Ingrese el chasis:");
        string chasis = Console.ReadLine();

        Console.WriteLine("Ingrese el motor:");
        string motor = Console.ReadLine();

        Console.WriteLine("Ingrese la marca de las características:");
        string marca = Console.ReadLine();

        Console.WriteLine("Ingrese el modelo de las características:");
        string modelo = Console.ReadLine();

        Console.WriteLine("Ingrese el color original de las características:");
        string colorOriginal = Console.ReadLine();

        Console.WriteLine("Ingrese la cilindrada de las características:");
        string cilindrada = Console.ReadLine();

        // Create an update definition to set the values of the fields
        var updateDefinition = Builders<BsonDocument>.Update
            .Set("Socio", socioId)
            .Set("Numero", numero)
            .Set("Chasis", chasis)
            .Set("Motor", motor)
            .Set("Caracteristicas.Marca", marca)
            .Set("Caracteristicas.Modelo", modelo)
            .Set("Caracteristicas.Color_original", colorOriginal)
            .Set("Caracteristicas.Cilindrada", cilindrada);

        // Update the document
        var result = collection.UpdateOne(filter, updateDefinition);

        // Check if the update was successful
        if (result.ModifiedCount > 0)
        {
            Console.WriteLine("Documento actualizado correctamente.");
        }
        else
        {
            Console.WriteLine("No se encontró ningún documento para actualizar.");
        }
    }

   
    static void DeleteSocio(IMongoCollection<BsonDocument> collection)
    {
        Console.WriteLine("Ingrese el ID del Socio a Eliminar:");
        string idSocio = Console.ReadLine();

        // Parse the idSocio string into an ObjectId
        ObjectId socioId;
        if (!ObjectId.TryParse(idSocio, out socioId))
        {
            Console.WriteLine("ID del Socio no válido.");
            return;
        }
        // Create a filter to find the document to update
        var filter = Builders<BsonDocument>.Filter.Eq("_id", socioId);


        // Delete the document
        collection.DeleteOne(filter);

        Console.WriteLine("Document deleted successfully.");
    }

    static void DeleteUnidad(IMongoCollection<BsonDocument> collection)
    {
        Console.WriteLine("Ingrese el ID de la Unidad a Eliminar:");
        string idUnidad = Console.ReadLine();

        // Parse the idUnidad string into an ObjectId
        ObjectId unidadId;
        if (!ObjectId.TryParse(idUnidad, out unidadId))
        {
            Console.WriteLine("ID de la Unidad no válido.");
            return;
        }

        // Create a filter to find the document to update
        var filter = Builders<BsonDocument>.Filter.Eq("_id", unidadId);

        collection.DeleteOne(filter);

        Console.WriteLine("Document deleted successfully.");
    }
}

public class Direccion
{
    public string Sector { get; set; }
    public string Calle_principal { get; set; }
    public string Calle_secundaria { get; set; }
    public string Numero { get; set; }
}

public class Socio
{
    public ObjectId Id { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Cedula { get; set; }
    public string Tipo_de_Licencia { get; set; }
    public Direccion Direccion { get; set; }
}

public class Unidad
{
    public ObjectId Id { get; set; }
    public ObjectId Socio { get; set; }
    public string Numero { get; set; }
    public string Chasis { get; set; }
    public string Motor { get; set; }
    public Caracteristicas Caracteristicas { get; set; }
}

public class Caracteristicas
{
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public string Color_original { get; set; }
    public string Cilindrada { get; set; }
}