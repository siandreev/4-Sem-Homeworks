module Logic
      
    /// функция для проверки, правильно ли стоят скобки в строке
    let checkString (str : string) =
        /// создаем список возможных скобок
        let bracketsList  =  ["(" ; ")" ; "{" ; "}" ; "[" ; "]"]
        let constOpeningBracketsList = ["(" ; "{" ; "["]
        
        /// создаем список из скобок в формате string в том порядке, в котором они идут в строке
        let onlyBracketList = (List.filter(fun x -> (List.contains x bracketsList)) (List.map string (Array.toList <| str.ToCharArray())))

        /// функция для получения открывающей скобки по закрывающей
        let getOpenByClose bracket =
            match bracket with
            | ")" -> Some "("
            | "}" -> Some "{"
            | "]" -> Some "["
            | _ -> None

        /// проходим по полученному списку из скобок: если встречаем открывающую- кладем ее в начало списка
        /// открывающих. Если закрывающую- сверяем, что она соответствует верхней скобке в списке открывающих.
        /// если что-то идет не так- возвращаем false, если успешно обнулили основной и вспомогательный списки- true
        let rec reducion workBracketsList openBracketsList =
           match workBracketsList with
           | [] -> 
               if openBracketsList = [] then
                   true
               else 
                   false
           | elem :: tail ->
               if List.contains(elem) constOpeningBracketsList then
                   reducion tail (elem :: openBracketsList)
               else 
                   match openBracketsList with
                   | [] -> false
                   | bracket :: tailOfBracketsList ->
                       if bracket = (getOpenByClose elem).Value then
                           reducion tail tailOfBracketsList
                       else
                           false
        reducion onlyBracketList []
        