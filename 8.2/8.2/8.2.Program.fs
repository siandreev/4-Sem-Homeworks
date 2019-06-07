module Logic        

    open System.Net
    open System.Text.RegularExpressions
    
    /// функция для скачивания html страницы и вывода информации о страницах, размещенных по ссылкам с нее
    let webFunction (url : string) =
        /// скачивание кода страницы и вывод на экран
        let getInfoAsync (url : string)=
            async {
                    let req = WebRequest.Create(url) 
                    use! resp = req.AsyncGetResponse() 
                    use stream = resp.GetResponseStream() 
                    use reader = new System.IO.StreamReader(stream)
                    let html = reader.ReadToEnd()
                    do printfn "url: %s -symbols count %d" url html.Length
                    return html               
            }
        let sourceText = Async.RunSynchronously <| getInfoAsync url
        let regex = Regex("<a href\s*=\s*\"?https?://[^\"]*\"?\s*>")

        /// список ссылок на исходной странице с обрезанным ненужным началом
        let dataList = 
            [for href in regex.Matches(sourceText) -> 
                let index1 = href.Value.IndexOf("\"")
                let index2 = href.Value.LastIndexOf("\"")
                href.Value.Substring(index1 + 1, index2 - index1 - 1)] |>
                    Seq.distinct |> Seq.toList |>
                        List.map(fun x -> x |> getInfoAsync)
        Async.Parallel dataList |> Async.RunSynchronously  
        
