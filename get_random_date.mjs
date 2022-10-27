const time_min = new Date(1990, 0, 1).getTime()
const time_max = new Date(2022, 10, 20).getTime()
const time_interval = time_max - time_min
export function get_random_date() {
  const random_value = time_min + Math.trunc(Math.random() * time_interval)
  return new Date(JSON.stringify(new Date(random_value)).substring(1, 11))
}
