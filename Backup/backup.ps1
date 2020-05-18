$collections = Invoke-RestMethod http://mysharedlist.azurewebsites.net/api/0_1/ListCollections

$date = Get-Date;
$date = $date.ToString("yyyy-MM-dd");
$dir = ".\\" + $date + "\\";
New-Item $dir -ItemType "directory"
foreach ($collection in $collections)
{
    $url = "http://mysharedlist.azurewebsites.net/api/0_1/Lists?skip=0&take=10000&listCollectionId=" + $collection.id;

    $list = Invoke-WebRequest $url

    $filename = $dir + $collection.id + ".txt"
    New-Item $filename
    Set-Content $filename $list.content
}