import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/documents.json' assert { type: 'json' };
const num_contrat = ['279834','279834F','279835','279835F','279836','279836F','279837','279837F','279838','279838F','279839','279839F','27984','279840','279840F','279841','279841F','279842','279842F','279843','279843F','279844','279844F','279845','279845F','279846','279846F','279847','279847F','279848','279848F','279849','279849F','27985','279850','279850F','279851','279851F','279852','279852F','279853','279853F','279854','279854F','279855','279855F','279856','279856F','279857','279857F','279858','279858F','279859','279859F','27986','279860','279860F','279861','279861F','279862','279862F','279863','279863F','279864','279864F','279865','279865F','279866',]
json_object.value = json_object.value.map((document, index) => ({
  ...document,
  numeroContrat: num_contrat[index]
}))
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')