﻿module Tests
    open Logic
    open FsCheck
    open NUnit.Framework
    
    [<Test>]
    let ``QuickCheckk``()=
        Check.Quick(fun x l -> (func x l) = (func'3 x l)) 


 