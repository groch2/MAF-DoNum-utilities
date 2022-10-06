import cotes from './cotes.json' assert { type: 'json' }
import familles from './familles.json' assert { type: 'json' }
import types_doc from './types_doc.json' assert { type: 'json' }
const document = {
  "documentId": "20220930171740074677704036",
  "assigneRedacteur": "ROD",
  "categoriesCote": "GESTION ASSOCIES",
  "categoriesFamille": "DOCUMENTS COCOON",
  "categoriesTypeDocument": "ACTE CESSION",
  "commentaire": "AA1C2854A89E4DDAABEF195CFB1529FC",
  "compteId": 6145,
  "adherent": "PAUL HUET",
  "coteDocumentId": 97,
  "dateDocument": "2022-09-30T22:00:00.000Z",
  "familleDocumentId": 19,
  "libelle": "409C100FAC9A4708B5E54B238F93365A",
  "numeroGc": "118218",
  "numeroSinistre": "",
  "sousDossierSinistre": "",
  "referenceSecondaire": "",
  "statut": "INDEXE",
  "typeDocumentId": 606,
  "important": false,
  "lienMiniKiosque": null,
  "depose": {
    "date": "2022-10-06T08:53:29.534Z",
    "displayName": "ROD - DESCHASEAUX Roch",
    "userCode": "ROD"
  },
  "isMiniKiosqueDoc": false,
  "chantierLibelle": null,
  "documentStatus": null
}
// Object.fromEntries(Object.entries(document).filter(([key, _]) => /id$/i.test(key)))
const _document = [document].map(({ coteDocumentId, familleDocumentId, typeDocumentId }) => ({ coteDocumentId, familleDocumentId, typeDocumentId }))[0]
console.log(_document)
const { libelle: familleDocumentLibelle } = familles.find(({ familleDocumentId }) => _document.familleDocumentId === familleDocumentId)
const { libelle: coteDocumentLibelle } = cotes.value.find(({ coteDocumentId, familleDocumentId: _familleDocumentId }) => coteDocumentId === _document.coteDocumentId && _familleDocumentId === _document.familleDocumentId)
const { libelle: typeDocumentLibelle } = types_doc.value.find(({ typeDocumentId }) => typeDocumentId === _document.typeDocumentId)
console.log({ familleDocumentLibelle, coteDocumentLibelle, typeDocumentLibelle })