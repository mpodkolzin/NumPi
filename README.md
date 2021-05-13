# NumPi
Bare bones DataFrame library for .net heavily inspired by Deedle

#### Basic usage:
##### Create DataFrame 
- from arbitraty json records:
```csharp
            var recs = new List<string>()
                {
                    @"{'Name': 'Test01', 'Value':10}",
                    @"{'Name': 'Test02', 'Value':20}",
                    @"{'Name': 'Test03', 'Value':30}",
                    @"{'Name': 'Test04', 'Value':40}"
                };
            var jobjects = recs.Select(r => JObject.Parse(r));
            var df = FrameExtensions.FromJson<int, string>(jobjects);
```
- from rows:
```csharp
var rows = Enumerable.Range(0, 10).Select(i =>
            {
                var sb = new SequenceBuilder<string, double>();

                sb.Add("Index", i);
                sb.Add("Sin", Math.Sin(i / 100.0));
                sb.Add("Cos", Math.Cos(i / 100.0));

                return new KeyValuePair<int, ObjectSequence<string>>(i, sb.ObjSequence);

            });

            var df = FrameExtensions.FromRows<int, string>(rows);
            var frameRows = df.Rows;
 ```
 - from collection of .net objects:
 ```csharp
             List<NestedRecord> testRecords = new List<NestedRecord>()
            {
                new NestedRecord() { InnerRecord =  new Record() { Name = "Test rec1", Timestamp = DateTime.Now, Value = 10}, Name = "Nest Rec1"},
                new NestedRecord() { InnerRecord =  new Record() { Name = "Test rec2", Timestamp = DateTime.Now, Value = 20}, Name = "Nest Rec2"},
                new NestedRecord() { InnerRecord =  new Record() { Name = "Test rec3", Timestamp = DateTime.Now, Value = 30}, Name = "Nest Rec3"},
                new NestedRecord() { InnerRecord =  new Record() { Name = "Test rec4", Timestamp = DateTime.Now, Value = 40}, Name = "Nest Rec4"}
            };

            var df = FrameReflectionUtils.ConvertRecordSequence(testRecords);

 ```
 ##### Basic operations with DataFrame
 
 - get row:
 ```csharp
            var df = GetTestDataFrame();
            var row = df.GetRow<object>(0);

            var valName = row.GetObject("Name"); //using accessor method
            var valTestCol = row["TestCol"]; //using indexer
 ```
 - merge 2 Dataframes:
```csharp
            var df1 = GetTestDataFrame();
            var df2 = GetTestDataFrame2().IndexRowsWith(Enumerable.Range((int)df1.RowCount, (int)df1.RowCount));
            var df_res = df1.Merge(new List<DataFrame<int, string>> { df2 });
```

#### Work in progress
 
