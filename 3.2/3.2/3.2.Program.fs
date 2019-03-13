module logic 
    // создаем размеченное объединение- дерево
    type Tree<'a> =
        | Tree of 'a * Tree<'a> * Tree<'a>
        | Tip of 'a

    // внутри основной функции mapTree рекурсивно проходим по всему дереву с корня: вызываем ту же вложенную
    // функцию от его поддеревьев до тех пор, пока не дойдем до листа, меняя при этом ключ
    let mapTree functionToMap someTree =
        let rec recMapTree f accTree =
            match accTree with
            | Tree(key, l, r) -> Tree(f key, recMapTree f l, recMapTree f r)
            | Tip(key) -> Tip(f key)
        recMapTree functionToMap someTree