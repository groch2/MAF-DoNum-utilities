import { tmpdir as get_temporary_directory } from 'os'
import * as fs from 'fs/promises'
import json_object from 'file://C:/Users/deschaseauxr/Documents/DONUM/document.json' assert { type: 'json' };
import { randomUUID as get_globally_unique_identifier } from 'crypto'
import { join as join_path } from 'path'
const temp_file_name = `${get_globally_unique_identifier({ disableEntropyCache: true })}.txt`
const temp_file_path = join_path(get_temporary_directory(), temp_file_name)
const case_insensitive_strings_compare =
  (a, b) => a.localeCompare(b, undefined, { sensitivity: 'accent' })
const alphabetically_sorted_object_properties =
  Object.keys(json_object).sort(case_insensitive_strings_compare).join('\r\n')
await fs.writeFile(temp_file_path, alphabetically_sorted_object_properties)
console.log({ temp_file_path })
process.exit(0)
