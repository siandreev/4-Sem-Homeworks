module Logic
    /// Функция для слияния двух спсков. Если они не пустые, то смотрим чей первый элемент меньше
    /// ставим его в голову, и проводим слияние для оставшихся списков
    let rec merge list1 list2 =
        match list1, list2 with 
        | [], [] -> [] 
        | [], someList ->  someList 
        | someList, [] -> someList
        | list1Head :: list1Tail, list2Head :: list2Tail ->
            if list1Head <= list2Head then
                list1Head :: merge list1Tail list2
            else
                list2Head :: merge list2Tail list1
     
    /// Основная функция. Если список болше чем из 1 элемента, то делим его на каждом шаге напополам 
    /// на два списка, сортируем каждый из них и потом сливаем их в один с помощью merge
    let mergeSort someList =
        let rec sort someList =
            match someList with
            | [] -> []
            | xs :: [] -> [xs]
            | _ -> 
                let middle = someList.Length / 2
                let (list1, list2) = List.splitAt middle someList
                merge (sort list1) (sort list2)
        sort someList

