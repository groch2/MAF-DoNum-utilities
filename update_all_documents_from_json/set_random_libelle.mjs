import * as fs from 'fs/promises'
import get_random_word from 'file://C:/Users/deschaseauxr/Documents/Donum/update_all_documents_from_json/get_random_word.mjs'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
console.log(get_random_word(10))
process.exit(0)
json_object.value = json_object.value.map((doc, index) => ({
  ...doc,
  libelle: `${`${index + 1}`.padStart(2, '0')} ${get_random_word(5)}`
}))
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')