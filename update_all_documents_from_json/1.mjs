import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
const familles_codes = [
  'DOCUMENTS COCOON',
  'DOCUMENTS CONTRAT',
  'DOCUMENTS EMOA',
  'DOCUMENTS PAPS',
  'DOCUMENTS PERSONNES'
]
json_object.value = json_object.value.map((doc, index) => ({
  ...doc,
  categoriesFamille: familles_codes[index < familles_codes.length ? index : Math.trunc(Math.random() * familles_codes.length)],
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