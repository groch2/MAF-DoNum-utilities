import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
const sens = [null, 'EMISSION', 'RECEPTION']
json_object.value = json_object.value.map((doc, index) => ({
  ...doc,
  sens: sens[index % 3]
}))
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')