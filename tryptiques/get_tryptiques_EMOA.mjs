import { areStringsEqualsCaseInsensitive } from '../string_compare.mjs'
import tryptiques from './tryptiques.json' assert { type: 'json' }
import * as fs from 'fs/promises'

const tryptiques_contrat =
  tryptiques.filter(({ famille }) => areStringsEqualsCaseInsensitive(famille, 'DOCUMENTS EMOA'))
await fs.writeFile('C:/Users/deschaseauxr/Documents/Donum/tryptiques/tryptiques_EMOA.json', JSON.stringify(tryptiques_contrat, undefined, 2))
