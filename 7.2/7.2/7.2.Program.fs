module Logic

    /// функция для преобразования строки в число: в зависимости от рещультата tryParse 
    /// возвращаем либо Some это число, либо None
    let stringParser (s : string) =
        let parseResult = System.Int32.TryParse(s)
        if  (fst parseResult) then
            Some (snd parseResult)
        else 
            None
        
    /// вычисление значения выражения, либо возврат сигнала о том, что оно записано неправильно 
    type StringCalcBuilder() =
        member this.Bind(s : string, f) =
            match stringParser s with
            | None -> None
            | Some value -> f value
        member this.Return(s) =
            Some(s)
       
           
