import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/Donum/documents.json' assert { type: 'json' };
import { get_random_date } from 'file://C:/Users/deschaseauxr/Documents/Donum/get_random_date.mjs'
json_object.value = json_object.value.map(document => {
  const document = {
    dateDocument: get_random_date(),
    deposeLe: get_random_date(),
    ...document,
  }
  return Object.fromEntries(Object.keys(document).sort().map(key => [key, document[key]]))
})
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')
