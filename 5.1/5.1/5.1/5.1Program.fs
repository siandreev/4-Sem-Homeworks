module logic
    
       
    let checkString (str : string) =
        // создаем список возможных скобок
        let bracketsList  =  ["(" ; ")" ; "{" ; "}" ; "[" ; "]"]
        // создаем список из скобок в формате string в том порядке, в котором они идут в строке
        let onlyBracketList = (List.filter(fun x -> (List.contains x bracketsList)) (List.map string (Array.toList <| str.ToCharArray())))

        // создаем из этого списка строку
        let onlyBracketsString = onlyBracketList |> List.fold (+) "" 
        let length = onlyBracketsString.Length

        // функция, которая убирает из строки вхождения пары открывающая-закрывающая скобка. В каждой строке с правильной расстановкой скобок такая пара есть в любой момент
        let rec reducion (s : string) length =
            let newStr = s.Replace("()", "").Replace("{}", "").Replace("[]", "")
            let newLength = newStr.Length

            // сверяем длину строки после удления: если изменилась более чем на 2- все норм, продолжаем вычеркивание до получения либо нулевой строки - тогда true,
            // либо если в какой-то момент не можем сократить, а строка не пустая- false
            match newLength with
            | _ when (length - newLength) >=2 -> reducion newStr newLength
            | 0 -> true
            | _ -> false
        reducion onlyBracketsString length
