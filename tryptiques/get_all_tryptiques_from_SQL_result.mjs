import { compare_strings_case_insensitive } from 'file://C:/Users/deschaseauxr/Documents/Donum/strings_compare.mjs'
import { readFile, writeFile } from 'fs/promises'

const raw_tryptiques = (await readFile('C:/Users/deschaseauxr/Documents/Donum/tryptiques/tryptiques.txt', 'utf-8')).split('\r\n')
raw_tryptiques.sort(compare_strings_case_insensitive)

const tryptiques = []
raw_tryptiques.forEach(line => {
  const [famille, cote, type] = line.split('\t')
  tryptiques.push({ famille, cote, type })
})

// console.debug(tryptiques)
// process.exit(0)

await writeFile('C:/Users/deschaseauxr/Documents/Donum/tryptiques/tryptiques.json', JSON.stringify(tryptiques, undefined, 2))

console.log('traitement terminé')
process.exit(0)
