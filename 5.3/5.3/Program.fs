module Logic
    open System
    open System.IO
    open System.Runtime.Serialization.Formatters.Binary
    
    // функция добавления пары в словарь
    let addToDictionary (dictionary : Map<string, string>) (name : string) (phone : string) =   (Map.add name phone) dictionary

    // поиск по имени с помощью Map.filter
    let findByName (dict : Map<string, string>) (name : string) =   Map.filter (fun key value -> key = name) dict

    // поиск по телефону с помощью Map.filter
    let findByPhone (dict : Map<string, string>) (phone : string) =   Map.filter (fun key value -> value = phone) dict 
      
    // запись в файл
    let writeValue outputStream (x: 'a) =
        let formatter = new BinaryFormatter()
        formatter.Serialize(outputStream, box x)

    let dictionaryToFile dictionary =     
         let fsOut = new FileStream(@"C:\5.3data\Data.dat", FileMode.Open)
         writeValue fsOut dictionary
         fsOut.Close()

    

    // функция, считывающая команду пользователя и возвращающая пару (словарь_1, словарь_2), где словарь_1 - что надо будет вывести на экран, словарь_2- что надо хранить в качестве базы
    let consoleReader (dictionary : Map<string, string>) (command : string) =
        match command with
        | _ when command.IndexOf("SELECT * FROM Dictionary WHERE phone = ") = 0 -> (findByPhone dictionary command.[39..command.Length - 1], dictionary)
        | _ when command.IndexOf("SELECT * FROM Dictionary WHERE name = ") = 0 -> (findByName dictionary command.[38..command.Length - 1], dictionary)
        | _ when command.IndexOf("INSERT INTO Dictionary VALUES (") = 0 -> 
                    let newKey = command.[command.IndexOf('(') + 1..command.IndexOf(',') - 1]
                    let newValue = command.[command.IndexOf(',') + 2..command.IndexOf(')') - 1]
                    let newDictionary = addToDictionary dictionary newKey newValue
                    (newDictionary, newDictionary)
        | _ when command = "SELECT * FROM Dictionary" -> (dictionary, dictionary)   
        | _ when command = "WRITE Dictionary TO FILE" -> dictionaryToFile dictionary; (Map.ofList["recording completed successfully",""], dictionary) 
        | _ when command = "READ Dictionary FROM FILE" ->
            // чтение из файла. Добавил именно сюда, потому что иначе тесты на чтение выдвали null
            let readValue (inputStream : Stream)  =
                let formatter = new BinaryFormatter()       
                let res = formatter.Deserialize(inputStream)
                unbox res 
            let dictionaryFromFile = 
               let fsIn = new FileStream(@"C:\5.3data\Data.dat",FileMode.Open)
               let res : Map<string,string> = readValue fsIn
               fsIn.Close()
               res 
            if File.Exists(@"C:\5.3data\Data.dat") then (dictionaryFromFile, dictionaryFromFile) 
            else Console.WriteLine("File not found") ; (dictionary, dictionary)    
        | _ when command = "EXIT" -> Environment.Exit(-1); (dictionary, dictionary)
        | _ -> (Map.ofList["Incorrect command",""], dictionary)

    // функция, получающая словарь (от consoleReader), который надо вывести и выводящая его на консоль
    let dictionaryWriter (dictionary : Map<string, string>) =   
        Console.WriteLine("--------------------------------------------------------------------")
        let rec elemWriter (list : List<string*string>) =
            match list with
            | x :: xs -> Console.WriteLine(fst x + " | " + snd x); elemWriter xs
            | [] -> None
        elemWriter <| (Map.toList dictionary) |> ignore
        Console.WriteLine("--------------------------------------------------------------------")
        Console.WriteLine()

    // основной "цикл". Узнает у consoleWriter -а что надо вывести на экран, а что надо хранить как базу. Выводит на экран с помощью dictionaryWriter -а и рекурсивно вызвает себя от нового словаря
    let rec consoleWriter dictionary =
        let dataPair = consoleReader dictionary <| Console.ReadLine()
        dictionaryWriter (fst dataPair)
        consoleWriter (snd dataPair)

    Console.ForegroundColor <- ConsoleColor.Red
    Console.WriteLine("TelephoneDictionary v.0.1")
    Console.WriteLine("For the dictionary to work correctly use the following commands")
    Console.WriteLine("SELECT * FROM Dictionary WHERE phone = /SomePhone")
    Console.WriteLine("SELECT * FROM Dictionary WHERE name = /SomeName")
    Console.WriteLine("INSERT INTO Dictionary VALUES (/SomeName, /SomePhone)")
    Console.WriteLine("SELECT * FROM Dictionary")
    Console.WriteLine("WRITE Dictionary TO FILE")
    Console.WriteLine("READ Dictionary FROM FILE")
    Console.WriteLine("EXIT")
    Console.ForegroundColor <- ConsoleColor.White

    // запуск программы с введенными тестовыми данными
    consoleWriter (Map.ofList [
        "Jeff", "89119974729";
        "Fred", "89119974729";
        "Mary", "89119977777" ]) 
 

  