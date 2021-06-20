import { AppState } from '../AppState'
import { api } from './AxiosService'
import { logger } from '../utils/Logger'
class BugsService {
  async getAll() {
    const res = await api.get('api/bugs')
    logger.log(res.data)
    AppState.bugs = res.data
    // split date at T to get date
  }

  async getOne(id) {
    const res = await api.get(`api/bugs/${id}`)
    logger.log(res.data)
    AppState.bugDetails = res.data
  }

  async create(bug) {
    const res = await api.post('api/bugs', bug)
    await this.getAll()
    return res.data.id
  }

  async close(id) {
    await api.delete(`api/bugs/${id}`)
    await this.getOne(id)
  }

  async edit(id, bug) {
    logger.log(bug)
    const res = await api.put(`api/bugs/${id}`, bug)
    logger.log('after')
    this.getOne(res.data.id)
  }
}

export const bugsService = new BugsService()
