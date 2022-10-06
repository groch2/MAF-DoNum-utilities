import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
const compte_id_list = [
  6117,
  6124,
  6127
]
json_object.value = json_object.value.map((doc, index) => ({
  ...doc,
  categoriesFamille: null,
  traiteLe: null,
  traitePar: null,
  vuLe: null,
  vuPar: null,
  qualiteValideeLe: null,
  qualiteValideePar: null,
  qualiteValideeValide: null,
  compteId: compte_id_list[index < compte_id_list.length ? index : Math.trunc(Math.random() * compte_id_list.length)]
}))
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')