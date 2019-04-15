module Tests 
    
    open NUnit.Framework
    open FsUnit
    open Logic

    [<Test>]
    let ``String "abccba" is palindrome that is why result should be true`` () =
        checkIsPalindrome "abccba" |> should equal true

    [<Test>]
    let ``String "abcba" is palindrome that is why result should be true`` () =
        checkIsPalindrome "abcba" |> should equal true

    
    [<Test>]
    let ``String "abcdba" is not palindrome that is why result should be false`` () =
        checkIsPalindrome "abcdba" |> should equal false

    [<Test>]
    let ``Empty string is palindrome that is why result should be true`` () =
        checkIsPalindrome "" |> should equal true 
