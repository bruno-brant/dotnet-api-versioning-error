# Api Versioning Error
Demonstrates an error with API versioning.

Run the tests to see what happens. 

# Solution

The solution is demonstrated at the branch [solution](https://github.com/bruno-brant/dotnet-api-versioning-error/tree/solution). You need to pass a key `version` to `routeValues` containing an instance of `ApiVersion` that points to the desired version. 

Either capture the current version and pass it back:

```csharp
[HttpPost]
public ActionResult Post([FromBody] Pet pet, ApiVersion version)
{
    if (pet.Id is null)
    {
        lock (_petStorage)
        {
            if (_petStorage.Count == 0)
            {
                pet.Id = 1;
            }
            else
            {
                pet.Id = _petStorage.Keys.Max() + 1;
            }
        }
    }

    _petStorage.Add(pet.Id.Value, pet);

    return CreatedAtAction(nameof(GetById), new { pet.Id, version = version.ToString() }, pet);
}
```

Or define the desired version by creating an instance of `ApiVersion`:

```csharp
[HttpPost]
public ActionResult Post([FromBody] Pet pet)
{
    if (pet.Id is null)
    {
        lock (_petStorage)
        {
            if (_petStorage.Count == 0)
            {
                pet.Id = 1;
            }
            else
            {
                pet.Id = _petStorage.Keys.Max() + 1;
            }
        }
    }

    _petStorage.Add(pet.Id.Value, pet);

    return CreatedAtAction(nameof(Get), new { pet.Id, version = new ApiVersion(1, 0).ToString() }, pet);
}
```
