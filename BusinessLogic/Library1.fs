namespace BusinessLogic

open FSharp.Data
open System.Linq

module XmlFactory = 

    //member MyObject
    

    

    //let parseXML xml = 
      //  {type MyObject = XmlProvider<"""<person></person>""">}

    //type XmlFactoryProvider(schema : string) =
    //    //let mutable schm = schema
      //  static member schm = schema

        //type XmlO = XmlProvider<XmlFactoryProvider.schm>

        
    type XMLObject = XmlProvider<"""<person><firstname>John</firstname><lastname>smith</lastname><props><address>123 street</address></props></person>""">
    
    let sample xml = 
        XMLObject.Parse(xml)

//type Class1() = 
//    member this.X = "F#"