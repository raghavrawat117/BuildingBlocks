# strings
GET KEYNAME
SET KEYNAME VALUE
DEL KEYNAME
## there are time to live as well

# json
JSON.GET KEYNAME
## notation is emp:{id}

## to set json, need to provide path for root i.e. in "."
JSON.SET emp:1 . '{ "empId": 1, "firstName": "Anibrata", "lastName": "Saha", "location": "Kolkata", "grade": 3, "experience": 4, "phoneNumber": "408-5555555", "workEmail": "anibrata.saha@laminar.com" }'

# index

## create
FT.CREATE empIdx:v2 
ON JSON PREFIX 1 emp: 
SCHEMA 
$.location AS location TEXT 
$.experience as experience NUMERIC
$.grade as grade NUMERIC

## join these lines using select all F1 
## and run in redis-cli to create index
FT.CREATE empIdx:v2 ON JSON PREFIX 1 emp: SCHEMA $.location AS location TEXT $.experience as experience NUMERIC $.grade as grade NUMERIC

## list the indrex created
FT._List

## define the index
FT.INFO 'empIdx:v2'

## searches with query
FT.SEARCH 'empIdx:v2' @location:(Banglore)
FT.SEARCH 'empIdx:v2' @experience:[2,5]
FT.SEARCH 'empIdx:v2' @grade:[3,5]

## multiple search
FT.SEARCH 'empIdx:v2' " @experience:[2,5] @location:(Banglore) "
FT.SEARCH 'empIdx:v2' " @experience:[2,5] @location:'New York' "
FT.SEARCH 'empIdx:v2' ' @experience:[2,5] @location:"New York" '