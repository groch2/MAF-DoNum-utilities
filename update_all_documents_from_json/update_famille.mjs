import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
const tryptiques = [
  {
    famille: 'DOCUMENTS CONTRAT',
    cote: 'PROCEDURE COLLECTIVE',
    type: 'A.R. REVENU SIGNE'
  },
  {
    famille: 'DOCUMENTS EMOA',
    cote: 'ATTESTATIONS ASSURANCES',
    type: 'ATTESTATION ASSURANCE'
  },
  {
    famille: 'DOCUMENTS PERSONNES',
    cote: 'COURRIERS',
    type: 'LETTRE AUTORISATION BANCAIRE'
  },
]
json_object.value = json_object.value.map((doc, index) => {
  const { famille, cote, type } = tryptiques[index % 3]
  return {
    ...doc,
    categoriesFamille: famille,
    categoriesCote: cote,
    categoriesTypeDocument: type,
  }
})
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')
