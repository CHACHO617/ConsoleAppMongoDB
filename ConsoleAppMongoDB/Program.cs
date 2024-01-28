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
                Console.Clear();
                Console.WriteLine("Choose an action:");
                Console.WriteLine("1. Crear Socio");
                Console.WriteLine("2. Crear Unidad");

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
                        CreateSocios(collectionSocio);
                        break;

                    case "2":
                        ReadSocio(collectionSocio);
                        CreateUnidades(collectionUnidad);
                        break;

                    case "3":
                        ReadSocio(collectionSocio);
                        break;

                    case "4":
                        ReadUnidad(collectionUnidad);
                        break;


                    case "5":
                        ReadSocio(collectionSocio);
                        UpdateSocio(collectionSocio);
                        break;

                    case "6":
                        ReadUnidad(collectionUnidad);
                        UpdateUnidad(collectionUnidad);
                        break;

                    case "7":
                        ReadSocio(collectionSocio);
                        DeleteSocio(collectionSocio);
                        break;

                    case "8":
                        ReadUnidad(collectionUnidad);
                        DeleteUnidad(collectionUnidad);
                        break;

                    case "0":
                        Console.WriteLine("Saliendo del programa");
                        return;

                    default:
                        Console.WriteLine("Por favor selecciona una opcion valida: ");
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


    static void CreateSocios(IMongoCollection<BsonDocument> collection)
    {
        Console.Clear();
        int count = 0;
        Console.WriteLine("Ingrese la cantidad de socios a ingresar:");
        string numIngresadoString = Console.ReadLine();
        int numIngresado;
        if (int.TryParse(numIngresadoString, out numIngresado))
        {
            // Parsing successful, numIngresado contains the integer value
            //Console.WriteLine("Number entered: " + numIngresado);
        }
        else
        {
            // Parsing failed, handle invalid input
            Console.WriteLine("Por favor ingresa un numero");
        }

        while (count < numIngresado)
        {
            count++;
            // Prompt the user for input
            Console.WriteLine("--------------------------------------------\nSocio " + count);
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
            Console.ReadLine();
        }
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


    static void CreateUnidades(IMongoCollection<BsonDocument> collection)
    {
        
        int count = 0;
        Console.WriteLine("Ingrese la cantidad de unidades a ingresar:");
        string numIngresadoString = Console.ReadLine();
        int numIngresado;
        if (int.TryParse(numIngresadoString, out numIngresado))
        {
            // Parsing successful, numIngresado contains the integer value
            //Console.WriteLine("Number entered: " + numIngresado);
        }
        else
        {
            // Parsing failed, handle invalid input
            Console.WriteLine("Ingresa un numero ");
        }

        while (count < numIngresado)
        {
            count++;
            // Prompt the user for input
            Console.WriteLine("--------------------------------------------\nUnidadad " + count);
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
            Console.ReadLine();
        }
    }


    static void ReadSocio(IMongoCollection<BsonDocument> collection)
    {
        // Read all documents in the collection
        Console.Clear();
        var documents = collection.Find(new BsonDocument()).ToList();

        Console.WriteLine("Listado de Socios:");
        Console.WriteLine("\n| " + "Id" +
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

        Console.ReadLine();
  
    }

    
    static void ReadUnidad(IMongoCollection<BsonDocument> collection)
    {
        Console.Clear();
        // Read all documents in the collection
        var documents = collection.Find(new BsonDocument()).ToList();

        Console.WriteLine("Listado de Unidades:");

        Console.WriteLine("\n| " + "Id Unidad" +
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

        Console.ReadLine();

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
            Console.ReadLine();
            return;
        }
        // Create a filter to find the document to update
        var filter = Builders<BsonDocument>.Filter.Eq("_id", socioId);

        
        Console.WriteLine("1. Toda la información del socio");
        Console.WriteLine("2. Nombre y Apellido");
        Console.WriteLine("3. Cedula");
        Console.WriteLine("4. Tipo de Licencia");
        Console.WriteLine("5. Dirección");
        Console.WriteLine("Selecciona que editar: ");
        string editar = Console.ReadLine();
        switch (editar)
        {
            case "1":
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
                    .Set("Direccion.Sector", sector)
                    .Set("Direccion.Calle_principal", callePrincipal)
                    .Set("Direccion.Calle_secundaria", calleSecundaria)
                    .Set("Direccion.Numero", numero);

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
                break;

            case "2":
                Console.WriteLine("Ingrese el nombre:");
                nombre = Console.ReadLine();

                Console.WriteLine("Ingrese el apellido:");
                apellido = Console.ReadLine();

                updateDefinition = Builders<BsonDocument>.Update
                    .Set("Nombre", nombre)
                    .Set("Apellido", apellido);

                // Update the document
                result = collection.UpdateOne(filter, updateDefinition);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Documento actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún documento para actualizar.");
                }
                break;

            case "3":
                Console.WriteLine("Ingrese la cédula:");
                cedula = Console.ReadLine();
                updateDefinition = Builders<BsonDocument>.Update
                   .Set("Cedula", cedula);

                // Update the document
                result = collection.UpdateOne(filter, updateDefinition);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Documento actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún documento para actualizar.");
                }

                break;


            case "4":
                Console.WriteLine("Ingrese el tipo de licencia:");
                tipoLicencia = Console.ReadLine();

                // Create an update definition to set the value of the "Nombre" field to "CAMBIADO"
                updateDefinition = Builders<BsonDocument>.Update
                    .Set("Tipo_de_Licencia", tipoLicencia);
                    

                // Update the document
                result = collection.UpdateOne(filter, updateDefinition);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Documento actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún documento para actualizar.");
                }

                break;

            case "5":
                Console.WriteLine("Ingrese el sector de la dirección:");
                sector = Console.ReadLine();

                Console.WriteLine("Ingrese la calle principal de la dirección:");
                callePrincipal = Console.ReadLine();

                Console.WriteLine("Ingrese la calle secundaria de la dirección:");
                calleSecundaria = Console.ReadLine();

                Console.WriteLine("Ingrese el número de la dirección:");
                numero = Console.ReadLine();

                // Create an update definition to set the value of the "Nombre" field to "CAMBIADO"
                updateDefinition = Builders<BsonDocument>.Update
                    .Set("Direccion.Sector", sector)
                    .Set("Direccion.Calle_principal", callePrincipal)
                    .Set("Direccion.Calle_secundaria", calleSecundaria)
                    .Set("Direccion.Numero", numero);

                // Update the document
                result = collection.UpdateOne(filter, updateDefinition);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Documento actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún documento para actualizar.");
                }
                break;


            case "0":
                Console.WriteLine("Exiting the application.");
                return;

            default:
                Console.WriteLine("Invalid choice. Please enter a valid option.");
                break;
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
            Console.ReadLine();
            return;
        }

        // Create a filter to find the document to update
        var filter = Builders<BsonDocument>.Filter.Eq("_id", unidadId);

        Console.WriteLine("1. Toda la información del socio");
        Console.WriteLine("2. Socio");
        Console.WriteLine("3. Número");
        Console.WriteLine("4. Chasis y Motor");
        Console.WriteLine("5. Características");
        Console.WriteLine("Selecciona que editar: ");

        string editar = Console.ReadLine();
        switch (editar)
        {
            case "1":
                // Prompt the user for input
                Console.WriteLine("Ingrese el ID del Socio:");
                string socioIdString = Console.ReadLine();
                ObjectId socioId;
                if (!ObjectId.TryParse(socioIdString, out socioId))
                {
                    Console.WriteLine("ID del Socio no válido.");
                    Console.ReadLine();
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

                break;

            case "2":
                // Prompt the user for input
                Console.WriteLine("Ingrese el ID del Socio:");
                socioIdString = Console.ReadLine();
                if (!ObjectId.TryParse(socioIdString, out socioId))
                {
                    Console.WriteLine("ID del Socio no válido.");
                    Console.ReadLine();
                    return;
                }

                // Create an update definition to set the values of the fields
                updateDefinition = Builders<BsonDocument>.Update
                    .Set("Socio", socioId);
                  
                // Update the document
                result = collection.UpdateOne(filter, updateDefinition);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Documento actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún documento para actualizar.");
                }

                break;

            case "3":
                Console.WriteLine("Ingrese el número:");
                numero = Console.ReadLine();


                // Create an update definition to set the values of the fields
                updateDefinition = Builders<BsonDocument>.Update
                    .Set("Numero", numero);

                // Update the document
                result = collection.UpdateOne(filter, updateDefinition);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Documento actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún documento para actualizar.");
                }
                break;


            case "4":
                Console.WriteLine("Ingrese el chasis:");
                chasis = Console.ReadLine();

                Console.WriteLine("Ingrese el motor:");
                motor = Console.ReadLine();

                // Create an update definition to set the values of the fields
                updateDefinition = Builders<BsonDocument>.Update
                    .Set("Chasis", chasis)
                    .Set("Motor", motor);

                // Update the document
                result = collection.UpdateOne(filter, updateDefinition);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Documento actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún documento para actualizar.");
                }

                break;

            case "5":
                Console.WriteLine("Ingrese la marca de las características:");
                marca = Console.ReadLine();

                Console.WriteLine("Ingrese el modelo de las características:");
                modelo = Console.ReadLine();

                Console.WriteLine("Ingrese el color original de las características:");
                colorOriginal = Console.ReadLine();

                Console.WriteLine("Ingrese la cilindrada de las características:");
                cilindrada = Console.ReadLine();

                // Create an update definition to set the values of the fields
                updateDefinition = Builders<BsonDocument>.Update
                    .Set("Caracteristicas.Marca", marca)
                    .Set("Caracteristicas.Modelo", modelo)
                    .Set("Caracteristicas.Color_original", colorOriginal)
                    .Set("Caracteristicas.Cilindrada", cilindrada);

                // Update the document
                result = collection.UpdateOne(filter, updateDefinition);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Documento actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún documento para actualizar.");
                }

                break;


            case "0":
                Console.WriteLine("Exiting the application.");
                return;

            default:
                Console.WriteLine("Invalid choice. Please enter a valid option.");
                break;
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
            Console.ReadLine();
            return;
        }
        // Create a filter to find the document to update
        var filter = Builders<BsonDocument>.Filter.Eq("_id", socioId);


        // Delete the document
        collection.DeleteOne(filter);
        Console.WriteLine("Document deleted successfully.");
        Console.ReadLine();


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
            Console.ReadLine();
            return;
        }

        // Create a filter to find the document to update
        var filter = Builders<BsonDocument>.Filter.Eq("_id", unidadId);

        collection.DeleteOne(filter);

        Console.WriteLine("Document deleted successfully.");
        Console.ReadLine();
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