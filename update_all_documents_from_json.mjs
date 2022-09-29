import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
json_object.value = json_object.value.map(doc => ({
  ...doc,
  traiteLe: null,
  traitePar: null,
  vuLe: null,
  vuPar: null,
  qualiteValideeLe: null,
  qualiteValideePar: null,
  qualiteValideeValide: null,
}))
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')