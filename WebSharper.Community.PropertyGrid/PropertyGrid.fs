﻿namespace WebSharper.Community.PropertyGrid

open WebSharper
open WebSharper.JavaScript
open WebSharper.UI.Next
open WebSharper.UI.Next.Client
open WebSharper.UI.Next.Html

[<JavaScript>]
type PropertyGrid =
    {
        Properties:ListModel<Key,PropertyItem>
    }
    static member Create =
        {
            Properties =  ListModel.Create (fun item ->item.Key) []
        }
    member x.Edit properties =
        x.Properties.Clear()
        x.Properties.AppendMany (properties |> List.map (PropertyItem.Create))
    member x.Render =
       let renderItem (item:PropertyItem)= 
           trAttr[]
              [
                 tdAttr [] [text item.Property.Name]
                 tdAttr [] [( item.Property.Render)]
                             
              ]
       divAttr [Attr.Class "propertyGrid"]
           [
               ListModel.View x.Properties
               |> Doc.BindSeqCachedBy (fun m -> m.Key) renderItem
           ]
    