module tests 
    
    open NUnit.Framework
    open FsUnit
    open logic

    [<Test>]
    let ``String "abccba" is palindrome that is why result should be true`` () =
        checkPalindrome "abccba" |> should equal true

    [<Test>]
    let ``String "abcba" is palindrome that is why result should be true`` () =
        checkPalindrome "abcba" |> should equal true

    
    [<Test>]
    let ``String "abcdba" is not palindrome that is why result should be false`` () =
        checkPalindrome "abcdba" |> should equal false

    [<Test>]
    let ``Empty string is palindrome that is why result should be true`` () =
        checkPalindrome "" |> should equal true 
