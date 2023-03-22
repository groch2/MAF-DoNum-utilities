import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/Donum/documents.json' assert { type: 'json' };
import tryptiques from 'file://C:/Users/deschaseauxr/Documents/Donum/tryptiques/tryptiques.json' assert { type: 'json' };
const cotes_utilisées = new Set()
json_object.value = json_object.value.map((doc) => {
  const { categoriesFamille: famille } = doc
  let index_tryptique_à_utiliser = tryptiques.findIndex((element) => element.famille === famille && !cotes_utilisées.has(`${famille}_${element.cote}`))
  if (index_tryptique_à_utiliser === -1) {
    [...cotes_utilisées].filter(cote => cote.startsWith(`${famille}_`)).forEach(cote => cotes_utilisées.delete(cote))
    index_tryptique_à_utiliser = tryptiques.findIndex((element) => element.famille === famille)
  }
  const { cote: cote_à_utiliser, type: type_à_utiliser } = tryptiques[index_tryptique_à_utiliser]
  cotes_utilisées.add(`${famille}_${cote_à_utiliser}`)
  return {
    ...doc,
    categoriesFamille: famille,
    categoriesCote: cote_à_utiliser,
    categoriesTypeDocument: type_à_utiliser,
  }
})
// console.log(json_object.value.map(({ categoriesFamille, categoriesCote }) => ({ categoriesFamille, categoriesCote })))
// process.exit(0)
const stringified = JSON.stringify(json_object, undefined, 2)
const file_path = 'C:/Users/deschaseauxr/Documents/DONUM/documents.json'
await fs.writeFile(file_path, stringified, { encoding: 'utf8' })
console.log('terminé')
