import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
import are_strings_equals_case_insensitive from './strings_compare.mjs'
const tryptiques = [
  {
    famille: 'DOCUMENTS COCOON',
    cote: 'ENCAISSEMENT',
    type: 'BORDEREAU DE REMISE EN BANQUE'
  },
  {
    famille: 'DOCUMENTS CONTRAT',
    cote: 'PROCEDURE COLLECTIVE',
    type: 'A.R. REVENU SIGNE'
  },
  {
    famille: 'DOCUMENTS EMOA',
    cote: 'ATTESTATIONS ASSURANCES',
    type: 'ATTESTATION ASSURANCE'
  }
]
json_object.value = json_object.value.map(doc => {
  const { cote, type } = tryptiques.find(({ famille }) => are_strings_equals_case_insensitive(famille, doc.categoriesFamille))
  return {
    ...doc,
    categoriesCote: cote,
    categoriesTypeDocument: type,
  }
})
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')
