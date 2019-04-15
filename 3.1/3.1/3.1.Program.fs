 module Logic 
      // делаем новый список из остатков чисел исходного списка по модулю 2, затем суммируем все остатки- это количество нечетных
      // чтобы найти количество четных, вычитаем из общего количества элементов количество нечетных.
      let evenCountMap list =
          List.length list - (List.sum (List.map (fun i -> abs (i % 2)) list))
   
      // фильтруем список, оставляя только четные элементы. Далее смотрим длину списка.
      let evenCountFilter list =
          List.length (List.filter (fun i -> i % 2 = 0) list)

      // с помощью List.fold сворачиваем лист и получаем количество нечетных. Далее вычитаем это число из общего количества элементов
      let evenCountFold list = 
          List.length list - List.fold (fun acc x -> acc + abs (x % 2)) 0 list