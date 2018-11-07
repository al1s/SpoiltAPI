# Spoilt API

* [Guidelines](#guidelines)
* [API Calls](#api-calls)

http://spoiltapi.azurewebsites.net

## Guidelines

This document provides information relating to the Spoilt API.

## API Calls
* List of Movies:
    * GET https://spoiltapi.azurewebsites.net/api/Movies
* Specific Movie:
    * GET https://spoiltapi.azurewebsites.net/api/Movies/{movieId}
* Search for Movie:
    * GET https://spoiltapi.azurewebsites.net/api/Movies/search?term={term}
* List of Spoilers:
    * GET https://spoiltapi.azurewebsites.net/api/Spoilers
* Specific Spoiler:
    * GET https://spoiltapi.azurewebsites.net/api/Spoilers/{spoilerId}
* Add a new spoilers
    * POST https://spoiltapi.azurewebsites.net/api/Spoilers
* Update a spoiler
    * PUT https://spoiltapi.azurewebsites.net/api/Spoilers/1



| HTTP METHOD | POST            | GET       | PUT         | DELETE |
| ----------- | --------------- | --------- | ----------- | ------ |
| CRUD OP     | CREATE          | READ      | UPDATE      | DELETE |
| /Movies       | Create new Movie | List Movies | ----------- | ----------- |
| /Movies/{id}  | -----------  | Get Movie   | If exists, update Movie; If not, error | ----------- |
| /Movies/search?term={term}       | ----------- | List Movies matching search term. | ----------- | ----------- |
| /Spoilers       | Create new Spoiler | List Spoilers | ----------- | ----------- |
| /Spoilers/{id}  | -----------  | Get Spoiler   | If exists, update Spoiler; If not, error | Delete Spoiler |


