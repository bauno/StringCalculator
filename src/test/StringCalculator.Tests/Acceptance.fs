module Tests.Acceptance

open Acceptance.Steps
open NUnit.Framework

[<SetUp>]
let init() =
    res <- None
    message <- None    
    logList.Clear()
    
[<Test>]
let ``I can sum two numbers`` () =
    ``Given I have entered`` "1,2" ``into the calculator``
    ``Then the result is`` 3


[<Test>]
let ``I can parse an empty string`` () =
    ``Given I have entered`` "" ``into the calculator``
    ``Then the result is`` 0

[<Test>]    
let ``I can sum more numbers``() =
    ``Given I have entered`` "12,22,90,11" ``into the calculator``
    ``Then the result is`` 135

[<Test>] 
let ``I cannot sum negative numbers`` () =
    ``Given I have entered`` "-1,2,3,-4" ``into the calculator``
    ``Then I fail with`` "Cannot add this negative numbers: -1 -4"

[<Test>]    
let ``Can change the delimiter``() =
    ``Given I have entered`` "//[;]\n1;2" ``into the calculator``
    ``Then the result is`` 3

[<Test>]    
let ``Will ignore numbers greater than 1000``() =
    ``Given I have entered`` "2,3,5,1001,7" ``into the calculator``
    ``Then the result is`` 17

[<Test>]    
let ``I can use delimiters of any length``() =
    ``Given I have entered`` "//[***]\n1***2***3" ``into the calculator``
    ``Then the result is`` 6
    
[<Test>]    
let ``I can use an arbitrary number of delimiters of any length``() =
    ``Given I have entered`` "//[*][%]\n2*2%3" ``into the calculator``
    ``Then the result is`` 7

[<Test>]    
let ``I can do it all``() =
    ``Given I have entered`` "//[**][%%%][$]\n2**2%%%3$4%%%1001$2000**3" ``into the calculator``
    ``Then the result is`` 14

[<EntryPoint>]
let main argv = 0
