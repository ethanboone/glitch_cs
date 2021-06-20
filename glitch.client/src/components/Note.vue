<template>
  <div class="row card-outline justify-content-center align-items-center my-2 flex-row" v-if="state.notes">
    <div class="col-6 text my-2">
      <img :src="note.creator.picture" height="35" alt="user icon" class="rounded-circle">
      {{ note.creator.name }}
    </div>
    <div class="col-3 text">
      {{ note.body }}
    </div>
    <!-- The account in the appstate must be assigned a value, and the user has to be authenticated
    from Auth0 and they have to be the creator of the note for the delete button to be rendered. -->
    <div class="col-2" v-if="state.account">
      <button class="btn text-danger" @click="deleteNote" v-if="state.user.isAuthenticated && state.note.creator.id === state.account.id">
        X
      </button>
    </div>
  </div>
</template>

<script>
import { bugsService } from '../services/BugsService'
import { logger } from '../utils/Logger'
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { notesService } from '../services/NotesService'

export default {
  name: 'Note',
  props: {
    note: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      notes: computed(() => AppState.notes)
    })
    return {
      state,
      async getAll() {
        try {
          await bugsService.getAll()
        } catch (error) {
          logger.error(error)
        }
      },
      async deleteNote() {
        try {
          if (window.confirm('Are you sure you want to remove this note?')) {
            await notesService.deleteNote(props.note)
          }
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
tr > td{
  border:2px solid #00ff00;
}

.text{
    color: #00ff00;
}

a:hover{
    text-decoration: none;
}

.card-outline {
  border: 1px solid #00ff00;
  border-radius: .25rem;
}

</style>
