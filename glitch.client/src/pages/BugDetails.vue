<template>
  <div class="row justify-content-center" v-if="state.bug">
    <div class="col-12 d-flex justify-content-end my-2" @click="close">
      <button class="btn btn-outline-green text" v-if="state.user.isAuthenticated && state.bug.closed === false">
        Close Bug
      </button>
    </div>
    <div class="col-10 d-flex justify-content-end">
      <button class="btn btn-outline-green text" @click="edit" v-if="state.user.isAuthenticated && state.bug.creator.id === state.account.id && bug.closed === false">
        Edit bug
      </button>
    </div>
    <div class="col-12 mb-4" v-if="state.edit === false">
      <p class="text mb-0">
        TITLE
      </p>
      <h2 class="text">
        {{ state.bug.title }}
      </h2>
      <div class="my-4">
        <p class="text mb-0">
          REPORTED BY
        </p>
        <h3 class="text">
          {{ state.bug.creator.name }}
        </h3>
      </div>
      <p class="text mb-0">
        STATUS
      </p><h3 class="text">
        <div v-if="state.bug.closed == true">
          Closed
        </div>
        <div v-else>
          Open
        </div>
      </h3>
    </div>
    <div class="col-12" v-else>
      <form class="form-group" @submit.prevent="editBug">
        <label class="text">Bug Title</label>
        <input type="text" class="form-control" v-model="state.editBug.title" required>
        <label class="text">Bug Description</label>
        <input type="text" class="form-control" v-model="state.editBug.description" required>
        <button type="submit" class="btn btn-outline-green text">
          Create
        </button>
      </form>
    </div>
    <div class="col-10 d-flex text-center card-outline py-3 my-4" v-if="state.bug.description">
      <p class="text my-0 py-0">
        {{ state.bug.description }}
      </p>
    </div>
    <div class="col-10">
      <div class="d-flex justify-content-center align-items-center">
        <h2 class="text mx-3">
          Notes
        </h2>
        <div>
          <button class="btn btn-outline-green text" data-toggle="modal" data-target="#note-modal" @click="state.toggle = !state.toggle" v-if="state.user.isAuthenticated">
            +
          </button>
        </div>
      </div>
      <form class="form-group" @submit.prevent="create" v-if="state.toggle">
        <label class="text">Note Message</label>
        <input type="text" class="form-control" v-model="state.newNote.body" required>
        <button type="submit" class="btn btn-outline-green text" v-if="state">
          Create
        </button>
      </form>
    </div>
    <div class="col-8 my-2" v-if="state.notes[0]">
      <Note v-for="note in state.notes" :key="note.id" :note="note" :bug="bug"></note>
    </div>
  </div>
</template>

<script>
import { reactive, computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { bugsService } from '../services/BugsService'
import { logger } from '../utils/Logger'
import { useRoute } from 'vue-router'
import { notesService } from '../services/NotesService'

export default {
  name: 'BugDetails',
  setup() {
    const route = useRoute()
    const state = reactive({
      bug: computed(() => AppState.bugDetails),
      notes: computed(() => AppState.notes),
      newNote: { bug: route.params.id },
      toggle: false,
      edit: false,
      editBug: {},
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    onMounted(async() => {
      try {
        await bugsService.getOne(route.params.id)
        await notesService.getAll(route.params.id)
        logger.log(state.notes)
      } catch (error) {

      }
    })
    return {
      state,
      async getBugs() {
        try {
          await bugsService.getAll()
        } catch (error) {
          logger.error(error)
        }
      },
      async create() {
        try {
          await notesService.create(state.newNote)
          state.toggle = false
        } catch (error) {
          logger.error(error)
        }
      },
      async close() {
        try {
          if (window.confirm('Are you sure you want to close this bug? This cannot be undone and the bug cannot be edited after it is closed.')) {
            await bugsService.close(route.params.id)
          }
        } catch (error) {
          logger.error(error)
        }
      },
      edit() {
        state.edit = !state.edit
      },
      async editBug() {
        try {
          state.editBug.closed = state.bug.closed
          state.editBug.creator = state.account
          await bugsService.edit(route.params.id, state.editBug)
        } catch (error) {
          logger.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
.text{
    color: #00ff00;
}

.btn-outline-green{
    border-color: #00ff00;
}

.card-outline {
  border: 1px solid #00ff00;
  border-radius: .25rem;
}
</style>
