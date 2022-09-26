const personne = {
  "userCrea": "string",
  "dateCrea": "2022-08-31T15:58:15.207Z",
  "userMod": "string",
  "dateMod": "2022-08-31T15:58:15.207Z",
  "personneID": 0,
  "adresse": "string",
  "adresseID": 0,
  "typePersonne": "string",
  "nom": "string",
  "prenom": "string",
  "raisonSociale": "string",
  "dateCreation": "2022-08-31T15:58:15.207Z",
  "dateNaissance": "2022-08-31T15:58:15.207Z",
  "lieuNaissance": "string",
  "siren": "string",
  "nic": "string",
  "nationaliteID": 0,
  "dateSituationJuridique": "2022-08-31T15:58:15.207Z",
  "mailing": true,
  "nomCommercial": "string",
  "formatAFNOR": "string",
  "nspec": true,
  "formeJuridiqueId": 0,
  "statutJuridiqueId": 0,
  "nomJF": "string",
  "rsAbregee": "string",
  "dateSituationCivile": "2022-08-31T15:58:15.207Z",
  "dateInscription": "2022-08-31T15:58:15.207Z",
  "matriculeRegional": 0,
  "matriculeNational": "string",
  "tva": "string",
  "partenaireProspectionAutorisee": true,
  "bce": "string",
  "typePersonneId": 0,
  "qualite": {
    "qualiteId": 0,
    "name": "string"
  },
  "situationArchitecte": {
    "situationArchitecteId": 0,
    "name": "string"
  },
  "situationCivile": {
    "situationCivileId": 0,
    "name": "string"
  },
  "situationCommerciale": {
    "situationCommercialeId": 0,
    "name": "string"
  },
  "situationJuridique": {
    "situationJuridiqueId": 0,
    "name": "string"
  }
}
const properties = Object.keys(personne).sort((a, b) => a.localeCompare(b, undefined, { sensitivity: 'accent' }))
console.debug(properties)
