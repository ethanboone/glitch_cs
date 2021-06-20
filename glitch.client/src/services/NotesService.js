import { AppState } from '../AppState'
import { api } from './AxiosService'

class NotesService {
  async getAll(bug) {
    const res = await api.get(`api/bugs/${bug}/notes`)
    AppState.notes = res.data
  }

  async create(note) {
    await api.post('api/notes', note)
    await this.getAll(note.bug)
  }

  async deleteNote(note) {
    await api.delete(`api/notes/${note.id}`)
    await this.getAll(note.bug)
  }
}

export const notesService = new NotesService()
