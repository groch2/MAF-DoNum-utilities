import { areStringsEqualsCaseInsensitive } from '../string_compare.mjs'
import tryptiques from './tryptiques.json' assert { type: 'json' }
import * as fs from 'fs/promises'

const tryptiques_contrat =
  tryptiques.filter(({ famille }) => areStringsEqualsCaseInsensitive(famille, 'DOCUMENTS CONTRAT'))
await fs.writeFile('C:/Users/deschaseauxr/Documents/Donum/tryptiques/tryptiques_contrat.json', JSON.stringify(tryptiques_contrat, undefined, 2))
